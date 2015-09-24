using System.Collections.Generic;
using VTP2015.DataAccess.Identity;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Interfaces;

namespace VTP2015.ServiceLayer.Implementations
{
    public class AuthenticationFacade : IAuthenticationFacade
    {
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
            throw new System.NotImplementedException();
        }

        public string GetOpleiding(string email)
        {
            throw new System.NotImplementedException();
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