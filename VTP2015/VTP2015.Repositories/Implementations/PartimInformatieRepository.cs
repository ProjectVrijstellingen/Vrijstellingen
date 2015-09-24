using System.Linq;
using VTP2015.DataAccess;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;
using VTP2015.Repositories.Remote_Services;

namespace VTP2015.Repositories.Implementations
{
    public class PartimInformatieRepository : IPartimInformatieRepository
    {
        private readonly IDataAccessFacade _db;
        private readonly GenericRepository<PartimInformatie> _genericRepository;
        private readonly IBamaflexRepository _bamaflexRepository;
        private readonly IDocentRepository _docentRepository;

        public PartimInformatieRepository(IDataAccessFacade db, IBamaflexRepository bamaflexRepository, IDocentRepository docentRepository)
        {
            _docentRepository = docentRepository;
            _bamaflexRepository = bamaflexRepository;
            _db = db;
            _genericRepository = new GenericRepository<PartimInformatie>(db.Context);
        }

        public IQueryable<PartimInformatie> GetAangevraagdePartims(string email,int dossierId)
        {
            return _db.Context.Aanvragen
                .Where(a => a.DossierId == dossierId && a.Dossier.Student.Email == email)
                .Select(a => a.PartimInformatie);
        }

        public IQueryable<PartimInformatie> GetBeschikbarePartims(string email, int dossierId)
        {
            return _db.Context.Studenten
                .Where(s => s.Email == email)
                .SelectMany(s => s.PartimInformatie)
                .Except(GetAangevraagdePartims(email, dossierId));
        }

        public PartimInformatie GetBySuperCode(string superCode)
        {
            return _genericRepository.AsQueryable(p => p.SuperCode == superCode).First();
        }

        public bool SyncStudentPartims(string email, string academieJaar)
        {
            if (_db.Context.Studenten.Where(s => s.Email == email).SelectMany(s => s.Dossiers).Any(d => d.AcademieJaar == academieJaar)) return false;
            var student = _db.Context.Studenten.First(x => x.Email == email);
            var bamaflexInformatie = _bamaflexRepository.GetPartimInformatieList(student.StudentId, academieJaar);

            foreach (var partimInformatie in bamaflexInformatie.Where(partimInformatie => !IsStudentLinkedToPartim(partimInformatie, student)))
            {
                if (IsPartimCached(partimInformatie, student))
                {
                    var informatie = partimInformatie;
                    student.PartimInformatie.Add(_genericRepository.AsQueryable(p => p.SuperCode == informatie.Supercode.Supercode1).First());
                    continue;
                }
                
                var partim = new PartimInformatie
                {
                    SuperCode = partimInformatie.Supercode.Supercode1,
                    Docent = _db.Context.Docenten.First(d => d.Email == "docent@howest.be")
                };

                if (student.PartimInformatie.Any(m => m.Partim.PartimId == partimInformatie.Partim.Id))
                {
                    partim.Partim = student.PartimInformatie
                        .Select(p => p.Partim)
                        .First(m => m.PartimId == partimInformatie.Partim.Id);
                }
                else if (_db.Context.Partims.Any(p => p.PartimId == partimInformatie.Partim.Id))
                {
                    partim.Partim = _db.Context.Partims.First(m => m.PartimId == partimInformatie.Partim.Id);
                }
                else
                {
                    partim.Partim = new Partim { PartimId = partimInformatie.Partim.Id, Naam = partimInformatie.Partim.Naam };

                }

                if (student.PartimInformatie.Any(m => m.Module.ModuleId == partimInformatie.Module.Id))
                {
                    partim.Module = student.PartimInformatie
                        .Select(p => p.Module)
                        .First(m => m.ModuleId == partimInformatie.Module.Id);
                }
                else if (_db.Context.Modules.Any(m => m.ModuleId == partimInformatie.Module.Id))
                {
                    partim.Module = _db.Context.Modules.First(m => m.ModuleId == partimInformatie.Module.Id);
                }
                else
                {
                    partim.Module = new Module { ModuleId = partimInformatie.Module.Id, Naam = partimInformatie.Module.Naam };

                }

                student.PartimInformatie.Add(partim);
            }

            _db.Context.SaveChanges();
            return true;
        }

        private bool IsPartimCached(DataAccess.Bamaflex.PartimInformatie partim, Student student)
        {
            return _genericRepository.AsQueryable(p => p.SuperCode == partim.Supercode.Supercode1).Any() ||
                student.PartimInformatie.Any(p => p.SuperCode == partim.Supercode.Supercode1);
        }

        private bool IsStudentLinkedToPartim(DataAccess.Bamaflex.PartimInformatie partim, Student student)
        {
            return _db.Context.Studenten
                .Where(s => s.StudentId == student.StudentId)
                .SelectMany(s => s.PartimInformatie)
                .Any(p => p.SuperCode == partim.Supercode.Supercode1) ||
                student.PartimInformatie
                .Any(p => p.SuperCode == partim.Supercode.Supercode1);
        }
    }
}
