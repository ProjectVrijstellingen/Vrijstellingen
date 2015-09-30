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
        private readonly GenericRepository<PartimInformation> _genericRepository;
        private readonly IBamaflexRepository _bamaflexRepository;
        private readonly IDocentRepository _docentRepository;

        public PartimInformatieRepository(IDataAccessFacade db, IBamaflexRepository bamaflexRepository, IDocentRepository docentRepository)
        {
            _docentRepository = docentRepository;
            _bamaflexRepository = bamaflexRepository;
            _db = db;
            _genericRepository = new GenericRepository<PartimInformation>(db.Context);
        }

        public IQueryable<PartimInformation> GetAangevraagdePartims(string email,int dossierId)
        {
            return _db.Context.Aanvragen
                .Where(a => a.FileId == dossierId && a.File.Student.Email == email)
                .Select(a => a.PartimInformation);
        }

        public IQueryable<PartimInformation> GetBeschikbarePartims(string email, int dossierId)
        {
            return _db.Context.Studenten
                .Where(s => s.Email == email)
                .Select(s => s.Opleiding)
                .SelectMany(s => s.KeuzeTrajecten)
                .SelectMany(s => s.PartimInformatie)
                .Except(GetAangevraagdePartims(email, dossierId));
        }

        public PartimInformation GetBySuperCode(string superCode)
        {
            return _genericRepository.AsQueryable(p => p.SuperCode == superCode).First();
        }

        public bool SyncStudentPartims(string email, string academicYear)
        {
            if (_db.Context.Studenten.Where(s => s.Email == email).SelectMany(s => s.Dossiers).Any(d => d.AcademieJaar == academicYear)) return false;
            var student = _db.Context.Studenten.First(x => x.Email == email);
            if (!IsOpleidingCached(student.Opleiding, academicYear))
            {
                var keuzeTrajecten = _bamaflexRepository.GetKeuzeTrajecten(student.Opleiding);
                foreach(var keuzeTraject in keuzeTrajecten)
                {
                    var traject = _db.Context.KeuzeTraject.Add(
                        new KeuzeTraject
                        {
                            Name = keuzeTraject.Naam
                }
                        );
                    _db.Context.Opleidingen.Find(student.Opleiding).KeuzeTrajecten.Add(traject);
                    foreach (var supercode in keuzeTraject.Modules.SelectMany(x => x.Partims).Select(x => x.Supercode))
                    {
                        var partimInformation = _bamaflexRepository.GetPartimInformationBySupercode(supercode);
                        var partimInfo = new PartimInformatie
                {
                            SuperCode = partimInformation.Supercode.Supercode1,
                            Docent = _db.Context.Docenten.First(d => d.Email == "docent@howest.be")//Needs real input!!!
                };

                        if (_db.Context.Partims.Any(p => p.PartimId == partimInformation.Partim.Id))
                {
                            partimInfo.Partim = _db.Context.Partims.First(m => m.PartimId == partimInformation.Partim.Id);
                }
                else
                {
                            partimInfo.Partim = new Partim { PartimId = partimInformation.Partim.Id, Naam = partimInformation.Partim.Naam };

                }

                        if (_db.Context.Modules.Any(m => m.ModuleId == partimInformation.Module.Id))
                {
                            partimInfo.Module = _db.Context.Modules.First(m => m.ModuleId == partimInformation.Module.Id);
                }
                else
                {
                            partimInfo.Module = new Module { ModuleId = partimInformation.Module.Id, Naam = partimInformation.Module.Naam };

                }
                        traject.PartimInformatie.Add(partimInfo);
                        _genericRepository.Insert(partimInfo);
                    }
            }
            }

            _db.Context.SaveChanges();
            return true;
        }

        private bool IsOpleidingCached(Opleiding opleiding, string academicYear)
        {
            return _db.Context.Opleidingen.Find(opleiding).AcademicYear == academicYear;
        }

        private bool IsPartimCached(DataAccess.Bamaflex.PartimInformatie partim, Student student)
        {
            return student.Opleiding.KeuzeTrajecten.SelectMany(x => x.PartimInformatie).Any(p => p.SuperCode == partim.Supercode.Supercode1);
        }
    }
}
