using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTP2015.ServiceLayer.Authentication
{
    public class MockAuthenticationFacade : IAuthenticationFacade
    {
        public bool IsCounselor(string email)
        {
            return true;
        }

        public bool AuthenticateUserByEmail(string email, string password)
        {
            return true;
        }

        public void SyncStudentByUser(string email, string academicYear)
        {
            
        }

        public void SyncLecturer(string email)
        {
            throw new NotImplementedException();
        }
    }
}
