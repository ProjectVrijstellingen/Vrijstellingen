using System.Linq;
using VTP2015.DataAccess;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;

namespace VTP2015.Repositories.Implementations
{
    public class DossierRepository : IDossierRepository
    {
        private readonly GenericRepository<File> _genericRepository;
        private readonly IDataAccessFacade _db;

        public DossierRepository(IDataAccessFacade db)
        {
            _db = db;
            _genericRepository = new GenericRepository<File>(db.Context);
        }

        public IQueryable<File> GetAll()
        {
            return _genericRepository.GetAll();
        }

        //public IQueryable<File> GetByOpleiding(int opleidingId)
        //{
        //    return "TODO"
        //}

        public IQueryable<File> GetAllNonEmpty()
        {
            return _genericRepository.AsQueryable(d => d.Requests.Count > 0);
        }

        public File GetById(int dossierId)
        {
            return _genericRepository.AsQueryable(d => d.FileId == dossierId).First();
        }

        public IQueryable<File> GetByStudent(string email)
        {
            return _genericRepository.AsQueryable(d => d.Student.Email == email);
        }

        public File Insert(File entity)
        {
            return _genericRepository.Insert(entity);
        }

        public bool IsDossierFromStudent(string email, int dossierId)
        {
            return _genericRepository.AsQueryable(d => d.FileId == dossierId).Any(d => d.Student.Email == email);
        }

        public IQueryable<File> GetFromBegeleider(string email, string academiejaar)
        {
            var opleidingId = _db.Context.TrajectBegeleiders.First(t => t.Email == email).Opleiding.code;
            return
                _genericRepository.AsQueryable(
                    d => d.Requests.Count > 0 && d.AcademicYear == academiejaar && d.Student.EducationId == opleidingId);
        }
    }
}