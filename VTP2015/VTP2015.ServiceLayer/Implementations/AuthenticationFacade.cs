using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.Identity;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Interfaces;

namespace VTP2015.ServiceLayer.Implementations
{
    public class AuthenticationFacade : IAuthenticationFacade
    {

        private readonly Repository<Counselor> _counselorRepository;

        public AuthenticationFacade(Repository<Counselor> counselorRepository)
        {
            _counselorRepository = counselorRepository;
            
        }

        public IEnumerable<Counselor> Counselors { get; }
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
            Counselor c = new Counselor();
            c.Email = email;
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