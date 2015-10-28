﻿using System.Linq;
using VTP2015.ServiceLayer.Counselor.Models;

namespace VTP2015.ServiceLayer.Counselor
{
    public interface ICounselorFacade
    {
        IQueryable<RequestPartimInformation> GetRequests(); 
        string GetEducationNameByCounselorEmail(string email);
        IQueryable<Education> GetEducations();
        void ChangeEducation(string email, string educationName);
        IQueryable<File> GetFileByCounselorEmail(string email, string academicYear);
        void SendReminder(int aanvraagId);
    }
}

