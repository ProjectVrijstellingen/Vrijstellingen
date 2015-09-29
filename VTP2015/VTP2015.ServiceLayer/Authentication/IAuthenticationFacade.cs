using System.Collections.Generic;
using VTP2015.DataAccess.Identity;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Authentication
{
    public interface IAuthenticationFacade
    {
        IEnumerable<Entities.Counselor> Counselors { get; }
        bool IsBegeleider(string email);
        void RemoveBegeleider(string email);
        void AddBegeleider(string email);
        string GetOpleiding(string email);
        void ChangeOpleiding(string email, Education opleiding);
        bool AuthenticateUserByEmail(string email, string password);
        User GetUserByUsername(string email);
        void SyncStudentByUser(User user);

    }
}
