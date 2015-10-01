using System;
using VTP2015.DataAccess.Identity;

namespace VTP2015.DataAccess.ServiceRepositories
{
    public class MockIdentityRepository : IIdentityRepository
    {
        public User GetUserByEmail(string email)
        {
            return new User
            {
                DateOfBirth = DateTime.Now,              
            };
        }

        public bool AuthenticateUserByEmail(string email, string password)
        {
            return true;
        }
    }
}
