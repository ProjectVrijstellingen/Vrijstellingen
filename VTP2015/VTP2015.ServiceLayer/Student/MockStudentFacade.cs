using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.Entities;
using Evidence = VTP2015.ServiceLayer.Student.Models.Evidence;
using File = VTP2015.ServiceLayer.Student.Models.File;
using PartimInformation = VTP2015.ServiceLayer.Student.Models.PartimInformation;
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

        public void InsertEvidence(Evidence evidence)
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
                    Editable = true,
                    Id = 1,
                    Education = "Toegepaste Informatica",
                    StudentMail = "sam.de.creus@student.howest.be"
                },
                new File
                {
                    AcademicYear = "2015-16",
                    DateCreated = DateTime.Now,
                    Editable = true,
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
                new Evidence { Description = "first evidence", StudentMail = "sam.de.creus@student.howest.be", Path = " "} //TODO: Path
            };

            return evidence.AsQueryable();
        }

        public bool IsFileFromStudent(string email, int fileId)
        {
            return true;
        }

        public IQueryable<PartimInformation> GetPartims(string email, int fileId, PartimMode partimMode)
        {
            var partimInformation = new List<PartimInformation>
            {
                new PartimInformation
                {
                    ModuleId = 1,
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
                    Argumentation = "argumentation",
                    //Status = Status.Untreated,
                    //Evidence = new List<Evidence>().AsQueryable(),
                    FileId = 1,
                    LastChanged = DateTime.Now,
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
                Path = "",
                StudentMail = "sam.de.creus@student.howest.be"
            };
        }

        public PartimInformation GetPartimInformationBySuperCode(string superCode)
        {
            return new PartimInformation
            {
                ModuleName = "C#",
                ModuleId = 1
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

        public Education GetEducation(string studentMail)
        {
            throw new NotImplementedException();
        }
    }
}
