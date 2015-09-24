﻿using System.Linq;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer
{
    public interface IStudentFacade
    {
        string GetStudentCodeByEmail(string email);
        void InsertEvidence(Evidence evidence);
        bool IsEvidenceFromStudent(string email);
        bool IsRequestFromStudent(int fileId, string supercode, string email);
        void DeleteEvidence(Evidence evidenceId);
        IQueryable<File> GetFilesByStudentEmail(string email);
        IQueryable<Evidence> GetEvidenceByStudentEmail(string email);
        bool IsFileFromStudent(string email, int fileId);
        IQueryable<PartimInformation> GetAvailablePartims(string email, int fileId);
        IQueryable<PartimInformation> GetRequestedPartims(string email, int fileId);
        IQueryable<Request> GetRequestsByFileId(int fileId);
        bool SyncStudentPartims(string email, string academicYear);
        Evidence GetEvidenceById(int evidenceId);
        PartimInformation GetPartimInformationBySuperCode(string superCode);
        void InsertFile(File file);
        bool SyncRequestInFile(Request request);
        bool DeleteRequest(int fileId, string supercode);
    }
}
