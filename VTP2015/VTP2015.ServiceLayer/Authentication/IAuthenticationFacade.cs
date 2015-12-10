using System.Collections.Generic;
using VTP2015.DataAccess.Identity;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Authentication
{
    public interface IAuthenticationFacade
    {
        bool IsCounselor(string email);
        bool AuthenticateUserByEmail(string email, string password);
        void SyncStudentByUser(string email, string academicYear);
        void SyncLecturer(string email);
    }
}
