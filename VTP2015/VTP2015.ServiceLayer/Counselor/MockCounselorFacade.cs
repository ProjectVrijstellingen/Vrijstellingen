using System;
using System.Linq;
using VTP2015.ServiceLayer.Counselor.Models;

namespace VTP2015.ServiceLayer.Counselor
{
    public class MockCounselorFacade : ICounselorFacade
    {
        public IQueryable<Request> GetRequests()
        {
            throw new NotImplementedException();
        }

        public string GetEducationNameByCounselorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Education> GetEducations()
        {
            throw new NotImplementedException();
        }

        public void ChangeEducation(string email, string educationName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<File> GetFileByCounselorEmail(string email, string academicYear)
        {
            throw new NotImplementedException();
        }

        public void SendReminder(int aanvraagId)
        {
            throw new NotImplementedException();
        }
    }
}
