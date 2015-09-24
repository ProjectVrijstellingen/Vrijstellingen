using VTP2015.DataAccess.Identity;

namespace VTP2015.Repositories.Remote_Services
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityManagementWebservice _service = new IdentityManagementWebservice();
        public bool AuthenticateUserByEmail(string email, string password)
        {
            return _service.AuthenticateUserByEmail(email, password);
        }

        public User GetUserByUsername(string email)
        {
            return _service.GetUserByEmail(email);
        }
    }
}