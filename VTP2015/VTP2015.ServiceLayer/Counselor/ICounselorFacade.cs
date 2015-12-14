﻿using System.Linq;
using VTP2015.ServiceLayer.Counselor.Models;

namespace VTP2015.ServiceLayer.Counselor
{
    public interface ICounselorFacade
    {
        IQueryable<Request> GetRequests();
        File GetFileByFileId(int fileId);
        string GetEducationNameByCounselorEmail(string email);
        IQueryable<Education> GetEducations();
        void ChangeEducation(string email, string educationName);
        IQueryable<File> GetFilesByCounselorEmail(string email, string academicYear);
        FileView GetFile(int fileId);
        void SendReminder(int aanvraagId);
        void ChangeFileStatus(int fileId, int status);
        void RemovePartimFromFile(int partimInformationId);
    }
}

