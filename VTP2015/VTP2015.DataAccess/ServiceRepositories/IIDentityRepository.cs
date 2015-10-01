using VTP2015.DataAccess.Identity;

namespace VTP2015.DataAccess.ServiceRepositories
{
    public interface IIdentityRepository
    {
        User GetUserByEmail(string email);
        bool AuthenticateUserByEmail(string email, string password);
    }
}
