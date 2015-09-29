using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.Identity;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Authentication
{
    public class AuthenticationFacade : IAuthenticationFacade
    {

        private readonly Repository<Entities.Counselor> _counselorRepository;

        public AuthenticationFacade(Repository<Entities.Counselor> counselorRepository)
        {
            _counselorRepository = counselorRepository;
            
        }

        public IEnumerable<Entities.Counselor> Counselors { get; }

        public bool IsBegeleider(string email)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveBegeleider(string email)
        {
            throw new System.NotImplementedException();
        }

        public void AddBegeleider(string email)
        {
            var c = new Entities.Counselor {Email = email};
            _counselorRepository.Insert(c);
        }

        public string GetOpleiding(string email)
        {
            return _counselorRepository.Table.First(s => s.Email == email).Education.ToString();
        }

        public void ChangeOpleiding(string email, Education opleiding)
        {
            throw new System.NotImplementedException();
        }

        public bool AuthenticateUserByEmail(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByUsername(string email)
        {
            throw new System.NotImplementedException();
        }

        public void SyncStudentByUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}