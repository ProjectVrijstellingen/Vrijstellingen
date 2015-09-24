using System.Linq;
using VTP2015.DataAccess;
using VTP2015.DataAccess.Bamaflex;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;

namespace VTP2015.Repositories.Implementations
{
    public class DossierRepository : IDossierRepository
    {
        private readonly GenericRepository<Dossier> _genericRepository;
        private readonly IDataAccessFacade _db;

        public DossierRepository(IDataAccessFacade db)
        {
            _db = db;
            _genericRepository = new GenericRepository<Dossier>(db.Context);
        }

        public IQueryable<Dossier> GetAll()
        {
            return _genericRepository.GetAll();
        }

        //public IQueryable<Dossier> GetByOpleiding(int opleidingId)
        //{
        //    return "TODO"
        //}

        public IQueryable<Dossier> GetAllNonEmpty()
        {
            return _genericRepository.AsQueryable(d => d.Aanvragen.Count > 0);
        }

        public Dossier GetById(int dossierId)
        {
            return _genericRepository.AsQueryable(d => d.DossierId == dossierId).First();
        }

        public IQueryable<Dossier> GetByStudent(string email)
        {
            return _genericRepository.AsQueryable(d => d.Student.Email == email);
        }

        public Dossier Insert(Dossier entity)
        {
            return _genericRepository.Insert(entity);
        }

        public bool IsDossierFromStudent(string email, int dossierId)
        {
            return _genericRepository.AsQueryable(d => d.DossierId == dossierId).Any(d => d.Student.Email == email);
        }


        public IQueryable<Dossier> GetFromBegeleider(string email, string academiejaar)
        {
            var opleidingId = _db.Context.TrajectBegeleiders.First(t => t.Email == email).Opleiding.OpleidingId;
            return
                _genericRepository.AsQueryable(
                    d => d.Aanvragen.Count > 0 && d.AcademieJaar == academiejaar && d.Student.OpleidingId == opleidingId);
        }
    }
}