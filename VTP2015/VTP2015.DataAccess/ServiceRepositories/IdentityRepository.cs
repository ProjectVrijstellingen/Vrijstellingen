using VTP2015.DataAccess.Identity;

namespace VTP2015.DataAccess.ServiceRepositories
{
    public class IdentityRepository : IIdentityRepository
    {
        readonly IdentityManagementWebservice _identityService = new IdentityManagementWebservice();

        public User GetUserByEmail(string email)
        {
            return _identityService.GetUserByEmail(email);
        }

        public bool AuthenticateUserByEmail(string email, string password)
        {
            return _identityService.AuthenticateUserByEmail(email, password);
        }
    }
}
