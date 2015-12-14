using System.Linq;
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
        void RemovePartimFromFile(int partimInformationId, int fileId);
        void SetFileStatusOpen(int fileId);
        void DeleteFile(int fileId);
        bool IsFileAvailable(int fileId);
    }
}

