using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess;
using VTP2015.Entities;
using VTP2015.Repositories.Interfaces;

namespace VTP2015.Repositories.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDataAccessFacade _db;

        public LoginRepository(IDataAccessFacade db)
        {
            _db = db;
        }

        public IEnumerable<Counselor> TrajectBegeleiders
        {
            get { return _db.Context.TrajectBegeleiders; }
        }

        public bool IsBegeleider(string email)
        {
            return _db.Context.TrajectBegeleiders.Count(trajectbegeleider => trajectbegeleider.Email.Equals(email)) > 0;
        }

        public void RemoveBegeleider(string email)
        {
            _db.Context.TrajectBegeleiders.Remove(_db.Context.TrajectBegeleiders.First(x => x.Email == email));
            _db.Context.SaveChanges();
        }

        public void AddBegeleider(string email)
        {
            _db.Context.TrajectBegeleiders.Add(new Counselor{Email = email});
            _db.Context.SaveChanges();
        }


        public string GetOpleiding(string email)
        {
            return _db.Context.TrajectBegeleiders.First(x => x.Email == email).Opleiding == null ? "" : _db.Context.TrajectBegeleiders.First(x => x.Email == email).Opleiding.Name;
        }

        public void ChangeOpleiding(string email, Education education)
        {
            _db.Context.TrajectBegeleiders.First(x => x.Email == email).Opleiding = education;
            _db.Context.SaveChanges();
        }
    }
}