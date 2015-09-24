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
        private readonly GenericRepository<Request> _genericRepository;

        public AanvraagRepository(IDataAccessFacade db)
        {
            _db = db;
            _genericRepository = new GenericRepository<Request>(db.Context);
        }

        public IQueryable<Request> GetAll()
        {
            return _genericRepository.AsQueryable(a => a.RequestId > 0);
        }

        public IQueryable<Request> GetOnbehandeldeAanvragen(string email)
        {
            return _db.Context.Docenten.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.Requests)
                .Where(a => a.Status == Status.Untreated);
        }

        public IQueryable<Request> GetOnbehandeldeAanvragenDistinct(string email)
        {
            var aanvragen = GetOnbehandeldeAanvragen(email);

            var result = from aanvraag in aanvragen
                         where aanvraag.RequestId > 0
                         group aanvraag by aanvraag.File.Student.StudentId
                             into groups
                             select groups.FirstOrDefault();

            return result;
        }

        public string GetEmailByAanvraagId(int aanvraagId)
        {
            return _genericRepository.AsQueryable(a => a.RequestId == aanvraagId).First().PartimInformation.Lecturer.Email;
        }

        public Request GetAanvraagById(int aanvraagId)
        {
            return _genericRepository.AsQueryable(a => a.RequestId == aanvraagId).First();
        }

        public bool Beoordeel(int aanvraagId, bool isGoedgekeurd, string email)
        {
            var aanvraag = _genericRepository.AsQueryable(a => a.RequestId == aanvraagId).First();

            if (aanvraag.Status == Status.Untreated && aanvraag.PartimInformation.Lecturer.Email == email)
            {
                aanvraag.Status = isGoedgekeurd ? Status.Approved : Status.Rejected;
                _genericRepository.Update(aanvraag);
                return true;
            }
            return false;

        }

        public IQueryable<Request> GetByDossierId(int dossierId)
        {
            return _genericRepository.AsQueryable(a => a.FileId == dossierId);
        }

        public bool Delete(int dossierId, string supercode)
        {
            if (!_genericRepository.AsQueryable(d => d.FileId == dossierId && d.SuperCode == supercode).Any())
                return false;
            _genericRepository.Delete(_genericRepository.AsQueryable(d => d.FileId == dossierId && d.SuperCode == supercode).First());
            return true;
        }

        public bool SyncAanvraagInDossier(Request request)
        {
            // TODO: Checken of partim bij student hoort
          if (
                _db.Context.Aanvragen.Any(
                    x => x.FileId == request.FileId && x.SuperCode == request.PartimInformation.SuperCode))
            {
                var bestaandeAanvraag = _db.Context.Aanvragen.First(
                    x => x.FileId == request.FileId && x.SuperCode == request.PartimInformation.SuperCode);

                bestaandeAanvraag.LastChanged = request.LastChanged;
                bestaandeAanvraag.Argumentation = request.Argumentation;

                var verwijderdeBewijzen = bestaandeAanvraag.Evidence.Except(request.Evidence).ToList();

                var toegevoegdeBewijzen = request.Evidence.Except(bestaandeAanvraag.Evidence).ToList();

                verwijderdeBewijzen.ForEach(b => bestaandeAanvraag.Evidence.Remove(b));

                foreach (var b in toegevoegdeBewijzen)
                {
                    if (_db.Context.Entry(b).State == EntityState.Detached)
                    {
                        _db.Context.Bewijzen.Attach(b);
                    }
                    bestaandeAanvraag.Evidence.Add(b);
                }
                _genericRepository.Update(bestaandeAanvraag);
            }
            else
            {
                _genericRepository.Insert(request);
            }
            return true;
        }
    }
}