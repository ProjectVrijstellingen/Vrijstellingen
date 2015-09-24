using System;
using System.Linq;
using VTP2015.DataAccess;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;
using VTP2015.Repositories.Remote_Services;

namespace VTP2015.Repositories.Implementations
{
    public class DocentRepository:IDocentRepository
    {
        private readonly IDataAccessFacade _db;
        private readonly GenericRepository<Lecturer> _genericRepository;
        private readonly IBamaflexRepository _bamaflexRepository;

        public DocentRepository(IDataAccessFacade db, IBamaflexRepository bamaflexRepository)
        {
            _bamaflexRepository = bamaflexRepository;
            _db = db;
            _genericRepository = new GenericRepository<Lecturer>(db.Context);
        }

        public Lecturer GetByEmail(string email)
        {
            return !EmailExists(email) ? null : _genericRepository.AsQueryable(d => d.Email == email).First();
        }

        public Lecturer AddDocent(string supercode)
        {
            var email = _bamaflexRepository.GetDocentFromPartim(supercode);
            if(EmailExists(email)) return _genericRepository.AsQueryable(x => x.Email == email).First();
            return _genericRepository.Insert(new Lecturer
            {
                Email = email,
                InfoMail = DateTime.Now,
                WarningMail = DateTime.Now
            });
        }

        public void ChangeInfoTime(string email, DateTime infoTime)
        {
            _db.Context.Docenten.First(d => d.Email == email).InfoMail = infoTime;
            _db.Context.SaveChanges();
        }

        public void ChangeWarningTime(string email, DateTime warningTime)
        {
            _db.Context.Docenten.First(d => d.Email == email).WarningMail = warningTime;
            _db.Context.SaveChanges();
        }

        public bool EmailExists(string email)
        {
            return _genericRepository.AsQueryable(d => d.Email == email).Any();
        }
    }
}
