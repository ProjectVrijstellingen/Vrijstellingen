using VTP2015.DataAccess.Identity;

namespace VTP2015.Repositories.Remote_Services
{
    public interface IIdentityRepository
    {
        bool AuthenticateUserByEmail(string email, string password);
        User GetUserByUsername(string email);
    }
}
