﻿using System.Linq;
using VTP2015.Entities;
using Evidence = VTP2015.ServiceLayer.Student.Models.Evidence;
using File = VTP2015.ServiceLayer.Student.Models.File;
using PartimInformation = VTP2015.ServiceLayer.Student.Models.PartimInformation;
using Request = VTP2015.ServiceLayer.Student.Models.Request;

namespace VTP2015.ServiceLayer.Student
{
    public interface IStudentFacade
    {
        string GetStudentCodeByEmail(string email);
        void InsertEvidence(Evidence evidence);
        bool IsEvidenceFromStudent(string email);
        bool IsRequestFromStudent(int fileId, string supercode, string email);
        bool DeleteEvidence(int evidenceId, string mapPath);
        IQueryable<File> GetFilesByStudentEmail(string email);
        IQueryable<Evidence> GetEvidenceByStudentEmail(string email);
        bool IsFileFromStudent(string email, int fileId);
        IQueryable<PartimInformation> GetPartims(string email, int fileId, PartimMode partimMode);
        IQueryable<Request> GetRequestsByFileId(int fileId);
        bool SyncStudentPartims(string email, string academicYear);
        Evidence GetEvidenceById(int evidenceId);
        PartimInformation GetPartimInformationBySuperCode(string superCode);
        int InsertFile(File file);
        bool SyncRequestInFile(Request request);
        bool DeleteRequest(int fileId, string supercode);
        Education GetEducation(string studentMail);
    }
}
