using System.Linq;
using VTP2015.ServiceLayer.Student.Models;

namespace VTP2015.ServiceLayer.Student
{
    public interface IStudentFacade
    {
        string GetStudentCodeByEmail(string email);
        void InsertEvidence(Evidence evidence, string studentMail);
        bool IsEvidenceFromStudent(string email);
        bool IsRequestFromStudent(int fileId, int requestId, string email);
        bool DeleteEvidence(int evidenceId, string mapPath);
        IQueryable<File> GetFilesByStudentEmail(string email);
        IQueryable<Evidence> GetEvidenceByStudentEmail(string email);
        bool IsFileFromStudent(string email, int fileId);
        IQueryable<PartimInformation> GetPartims(int fileId, PartimMode partimMode);
        IQueryable<Request> GetRequestByFileId(int fileId);
        bool SyncStudentPartims(string email, string academicYear);
        Evidence GetEvidenceById(int evidenceId);
        int InsertFile(File file);
        bool SyncRequestInFile(Request request);
        bool DeleteRequest(int fileId, int requestId);
        string GetEducation(string studentMail);
        string AddRequestInFile(int fileId, string code);
        FileStatus GetFileStatus(int fileId);
        IQueryable<Models.Student> GetStudent(string email);
        void SyncStudent(string email, string academicYear);
        IQueryable<PrevEducation> GetPrevEducationsByStudentEmail(string email);
        void InsertPrevEducation(string education, string email);
        bool DeleteEducation(int educationId);
        string[] SumbitFile(string email, string academicYear);
    }
}