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

        public IEnumerable<TrajectBegeleider> TrajectBegeleiders
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
            _db.Context.TrajectBegeleiders.Add(new TrajectBegeleider{Email = email});
            _db.Context.SaveChanges();
        }


        public string GetOpleiding(string email)
        {
            return _db.Context.TrajectBegeleiders.First(x => x.Email == email).Opleiding == null ? "" : _db.Context.TrajectBegeleiders.First(x => x.Email == email).Opleiding.Naam;
        }

        public void ChangeOpleiding(string email, Opleiding opleiding)
        {
            _db.Context.TrajectBegeleiders.First(x => x.Email == email).Opleiding = opleiding;
            _db.Context.SaveChanges();
        }
    }
}