using System.Data.Entity;
using System.Linq;
using VTP2015.DataAccess;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;

namespace VTP2015.Repositories.Implementations
{
    public class AanvraagRepository : IAanvraagRepository
    {
        private readonly IDataAccessFacade _db;
        private readonly GenericRepository<Aanvraag> _genericRepository;

        public AanvraagRepository(IDataAccessFacade db)
        {
            _db = db;
            _genericRepository = new GenericRepository<Aanvraag>(db.Context);
        }

        public IQueryable<Aanvraag> GetAll()
        {
            return _genericRepository.AsQueryable(a => a.AanvraagId > 0);
        }

        public IQueryable<Aanvraag> GetOnbehandeldeAanvragen(string email)
        {
            return _db.Context.Docenten.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformatie)
                .SelectMany(p => p.Aanvragen)
                .Where(a => a.Status == Status.Onbehandeld);
        }

        public IQueryable<Aanvraag> GetOnbehandeldeAanvragenDistinct(string email)
        {
            var aanvragen = GetOnbehandeldeAanvragen(email);

            var result = from aanvraag in aanvragen
                         where aanvraag.AanvraagId > 0
                         group aanvraag by aanvraag.Dossier.Student.StudentId
                             into groups
                             select groups.FirstOrDefault();

            return result;
        }

        public string GetEmailByAanvraagId(int aanvraagId)
        {
            return _genericRepository.AsQueryable(a => a.AanvraagId == aanvraagId).First().PartimInformatie.Docent.Email;
        }

        public Aanvraag GetAanvraagById(int aanvraagId)
        {
            return _genericRepository.AsQueryable(a => a.AanvraagId == aanvraagId).First();
        }

        public bool Beoordeel(int aanvraagId, bool isGoedgekeurd, string email)
        {
            var aanvraag = _genericRepository.AsQueryable(a => a.AanvraagId == aanvraagId).First();

            if (aanvraag.Status == Status.Onbehandeld && aanvraag.PartimInformatie.Docent.Email == email)
            {
                aanvraag.Status = isGoedgekeurd ? Status.Goedgekeurd : Status.Afgekeurd;
                _genericRepository.Update(aanvraag);
                return true;
            }
            return false;

        }

        public IQueryable<Aanvraag> GetByDossierId(int dossierId)
        {
            return _genericRepository.AsQueryable(a => a.DossierId == dossierId);
        }

        public bool Delete(int dossierId, string supercode)
        {
            if (!_genericRepository.AsQueryable(d => d.DossierId == dossierId && d.SuperCode == supercode).Any())
                return false;
            _genericRepository.Delete(_genericRepository.AsQueryable(d => d.DossierId == dossierId && d.SuperCode == supercode).First());
            return true;
        }

        public bool SyncAanvraagInDossier(Aanvraag aanvraag)
        {
            // TODO: Checken of partim bij student hoort
          if (
                _db.Context.Aanvragen.Any(
                    x => x.DossierId == aanvraag.DossierId && x.SuperCode == aanvraag.PartimInformatie.SuperCode))
            {
                var bestaandeAanvraag = _db.Context.Aanvragen.First(
                    x => x.DossierId == aanvraag.DossierId && x.SuperCode == aanvraag.PartimInformatie.SuperCode);

                bestaandeAanvraag.LastChanged = aanvraag.LastChanged;
                bestaandeAanvraag.Argumentatie = aanvraag.Argumentatie;

                var verwijderdeBewijzen = bestaandeAanvraag.Bewijzen.Except(aanvraag.Bewijzen).ToList();

                var toegevoegdeBewijzen = aanvraag.Bewijzen.Except(bestaandeAanvraag.Bewijzen).ToList();

                verwijderdeBewijzen.ForEach(b => bestaandeAanvraag.Bewijzen.Remove(b));

                foreach (var b in toegevoegdeBewijzen)
                {
                    if (_db.Context.Entry(b).State == EntityState.Detached)
                    {
                        _db.Context.Bewijzen.Attach(b);
                    }
                    bestaandeAanvraag.Bewijzen.Add(b);
                }
                _genericRepository.Update(bestaandeAanvraag);
            }
            else
            {
                _genericRepository.Insert(aanvraag);
            }
            return true;
        }
    }
}