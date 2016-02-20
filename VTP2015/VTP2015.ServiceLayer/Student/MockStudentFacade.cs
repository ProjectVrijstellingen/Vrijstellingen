using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.Entities;
using PartimMode = VTP2015.ServiceLayer.Student.Models.PartimMode;
using Evidence = VTP2015.ServiceLayer.Student.Models.Evidence;
using File = VTP2015.ServiceLayer.Student.Models.File;
using FileStatus = VTP2015.ServiceLayer.Student.Models.FileStatus;
using PartimInformation = VTP2015.ServiceLayer.Student.Models.PartimInformation;
using PrevEducation = VTP2015.ServiceLayer.Student.Models.PrevEducation;
using Request = VTP2015.ServiceLayer.Student.Models.Request;
using Status = VTP2015.ServiceLayer.Student.Models.Status;

namespace VTP2015.ServiceLayer.Student
{
    public class MockStudentFacade : IStudentFacade
    {
        public string GetStudentCodeByEmail(string email)
        {
            return "";
        }

        public void InsertEvidence(Evidence evidence, string studentMail)
        {
            
        }

        public bool IsEvidenceFromStudent(string email)
        {
            return true;
        }

        public bool IsRequestFromStudent(int fileId, int requestId, string email)
        {
            return true;
        }

        public bool DeleteEvidence(int evidenceId, string mapPath)
        {
            return true;
        }

        public IQueryable<File> GetFilesByStudentEmail(string email)
        {
            var files = new List<File>
            {
                new File
                {
                    AcademicYear = "2015-16",
                    DateCreated = DateTime.Now,
                    FileStatus = FileStatus.InProgress,
                    Id = 1,
                    Education = "Toegepaste Informatica",
                    StudentMail = "sam.de.creus@student.howest.be"
                },
                new File
                {
                    AcademicYear = "2015-16",
                    DateCreated = DateTime.Now,
                    FileStatus = FileStatus.InProgress,
                    Id = 1,
                    Education = "Toegepaste Informatica",
                    StudentMail = "sam.de.creus@student.howest.be"
                }
            };

            return files.AsQueryable();
        }

        public IQueryable<Evidence> GetEvidenceByStudentEmail(string email)
        {
            var evidence = new List<Evidence>
            {
                new Evidence { Description = "first evidence", Path = " "} //TODO: Path
            };

            return evidence.AsQueryable();
        }

        public bool IsFileFromStudent(string email, int fileId)
        {
            return true;
        }

        public IQueryable<PartimInformation> GetPartims(int fileId, PartimMode partimMode)
        {
            var partimInformation = new List<PartimInformation>
            {
                new PartimInformation
                {
                    Code = "SDEV",
                    SuperCode = "",
                    ModuleName = "Software ontwikkeling",
                    PartimName = "C#"
                }
            };

            return partimInformation.AsQueryable();
        }

        public IQueryable<Request> GetRequestByFileId(int fileId)
        {
            var requests = new List<Request>
            {
                new Request
                {
                    //Argumentation = "argumentation",
                    //Status = Status.Untreated,
                    //Evidence = new List<Evidence>().AsQueryable(),
                    FileId = 1,
                    //PartimInformationId = 1,
                }
            };

            return requests.AsQueryable();
        }

        public bool SyncStudentPartims(string email, string academicYear)
        {
            return true;
        }       

        public Evidence GetEvidenceById(int evidenceId)
        {
            return new Evidence
            {
                Description = "description",
                Path = ""
            };
        }

        public PartimInformation GetPartimInformationBySuperCode(string superCode)
        {
            return new PartimInformation
            {
                ModuleName = "C#",
                Code = "SDEV"
            };
        }

        public int InsertFile(File file)
        {
            return 1;
        }

        public bool SyncRequestInFile(Request request)
        {
            return true;
        }

        public bool DeleteRequest(int fileId, int requestId)
        {
            return true;
        }

        public string GetEducation(string studentMail)
        {
            throw new NotImplementedException();
        }

        public string AddRequestInFile(int fileId, string code)
        {
            throw new NotImplementedException();
        }

        public FileStatus GetFileStatus(int fileId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.Student> GetStudent(string email)
        {
            throw new NotImplementedException();
        }

        public void SyncStudent(string email, string academicYear)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PrevEducation> GetPrevEducationsByStudentEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void InsertPrevEducation(string education, string email)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEducation(int educationId)
        {
            throw new NotImplementedException();
        }

        public string[] SumbitFile(string email, string academicYear)
        {
            throw new NotImplementedException();
        }
    }
}
