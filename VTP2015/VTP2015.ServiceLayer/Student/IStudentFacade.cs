using System.Linq;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Student
{
    public interface IStudentFacade
    {
        string GetStudentCodeByEmail(string email);
        void InsertEvidence(Models.Evidence evidence);
        bool IsEvidenceFromStudent(string email);
        bool IsRequestFromStudent(int fileId, string supercode, string email);
        bool DeleteEvidence(int evidenceId, string mapPath);
        IQueryable<File> GetFilesByStudentEmail(string email);
        IQueryable<Evidence> GetEvidenceByStudentEmail(string email);
        bool IsFileFromStudent(string email, int fileId);
        IQueryable<PartimInformation> GetAvailablePartims(string email, int fileId);
        IQueryable<PartimInformation> GetRequestedPartims(string email, int fileId);
        IQueryable<Request> GetRequestsByFileId(int fileId);
        bool SyncStudentPartims(string email, string academicYear);
        Evidence GetEvidenceById(int evidenceId);
        PartimInformation GetPartimInformationBySuperCode(string superCode);
        int InsertFile(Models.File file);
        bool SyncRequestInFile(Models.Request request);
        bool DeleteRequest(int fileId, string supercode);
    }
}
