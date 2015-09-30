using System;
using System.Linq;
using VTP2015.DataAccess;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;
using VTP2015.Repositories.Remote_Services;

namespace VTP2015.Repositories.Implementations
{
    public class OpleidingRepository : IOpleidingRepository
    {
        private readonly IDataAccessFacade _db;
        private readonly GenericRepository<Opleiding> _genericRepository;
        private readonly IBamaflexRepository _bamaflexRepository;

        public OpleidingRepository(IDataAccessFacade db, IBamaflexRepository bamaflexRepository)
        {
            _db = db;
            _genericRepository = new GenericRepository<Opleiding>(db.Context);
            _bamaflexRepository = bamaflexRepository;

        }

        public Opleiding GetByEmail(string email)
        {
            return _db.Context.Studenten
                .Where(s => s.Email == email)
                .Select(s => s.Education).First();
        }

        public IQueryable<Opleiding> GetOpleidingen()
        {
            if(!_genericRepository.GetAll().Any()) SyncOpleidingen();
            return _genericRepository.GetAll();
        }

        private void SyncOpleidingen()
        {
            var opleidingen = _bamaflexRepository.GetOpleidingen();
            foreach (var entity in opleidingen.Select(opleiding => new Opleiding
            {
                code = opleiding.code,
                Name = opleiding.Name
            }))
            {
                _genericRepository.Insert(entity);
            }
        }
    }
}
