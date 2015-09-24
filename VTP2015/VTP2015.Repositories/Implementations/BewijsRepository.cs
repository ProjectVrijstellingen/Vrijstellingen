using System.Linq;
using VTP2015.DataAccess;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;

namespace VTP2015.Repositories.Implementations
{
    public class BewijsRepository : IBewijsRepository
    {
        private readonly IDataAccessFacade _db;
        private readonly GenericRepository<Bewijs> _genericRepository; 

        public BewijsRepository(IDataAccessFacade db)
        {
            _db = db;
            _genericRepository = new GenericRepository<Bewijs>(db.Context);
        }

        public bool IsBewijsFromStudent(string email)
        {
            return _genericRepository.AsQueryable(b => b.Student.Email == email).Any();
        }

        public Bewijs GetById(int bewijsId)
        {
            return _genericRepository.AsQueryable(b => b.BewijsId == bewijsId).First();
        }

        public IQueryable<Bewijs> GetByStudent(string email)
        {
            return _genericRepository.AsQueryable(b => b.Student.Email == email);
        }

        public string Delete(object id)
        {
            var bewijs = GetById((int)id);
            _genericRepository.Delete(bewijs);
            return bewijs.Path;
        }

        public Bewijs Insert(Bewijs entity)
        {
            return _genericRepository.Insert(entity);
        }
    }
}