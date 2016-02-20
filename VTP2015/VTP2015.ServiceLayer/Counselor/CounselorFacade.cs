using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Counselor.Mappings;
using VTP2015.ServiceLayer.Counselor.Models;
using VTP2015.ServiceLayer.Mail;
using Education = VTP2015.Entities.Education;
using Evidence = VTP2015.ServiceLayer.Counselor.Models.Evidence;
using File = VTP2015.Entities.File;
using Partim = VTP2015.ServiceLayer.Counselor.Models.Partim;
using PartimInformation = VTP2015.ServiceLayer.Counselor.Models.PartimInformation;
using PrevEducation = VTP2015.ServiceLayer.Counselor.Models.PrevEducation;
using Request = VTP2015.Entities.Request;

namespace VTP2015.ServiceLayer.Counselor
{
    public class CounselorFacade : ICounselorFacade
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<Education> _educationRepository;
        private readonly IRepository<Entities.Counselor> _counselorRepository;
        private readonly IRepository<File> _fileRepository;
        private readonly IRepository<RequestPartimInformation> _requestPartimInformationRepository;
        private readonly IRepository<Motivation> _motivationRepository;
        private readonly IRepository<Entities.PartimInformation> _partimInformationRepository;
        private readonly IRepository<Entities.Lecturer> _lecturerRepository; 

        public CounselorFacade(IUnitOfWork unitOfWork)
        {
            _requestRepository = unitOfWork.Repository<Request>();
            _educationRepository = unitOfWork.Repository<Education>();
            _counselorRepository = unitOfWork.Repository<Entities.Counselor>();
            _fileRepository = unitOfWork.Repository<File>();
            _motivationRepository = unitOfWork.Repository<Motivation>();
            _requestPartimInformationRepository = unitOfWork.Repository<RequestPartimInformation>();
            _partimInformationRepository = unitOfWork.Repository<Entities.PartimInformation>();
            _lecturerRepository = unitOfWork.Repository<Entities.Lecturer>();

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }

        public void RemovePartimFromFile(int partimInformationId, int fileId)
        {
            var requestPartimInformation =
                _requestPartimInformationRepository.Table
                .First(r => r.PartimInformationId == partimInformationId && r.Request.FileId == fileId);

            var request = requestPartimInformation.Request;
            _requestPartimInformationRepository.Delete(requestPartimInformation);

            if (request.RequestPartimInformations.Count < 1)
                _requestRepository.Delete(request);
            if(_fileRepository.GetById(fileId).Requests.Count < 1)
                _fileRepository.Delete(fileId);
        }

        public void SetFileStatusOpen(int fileId)
        {
            _fileRepository.GetById(fileId).FileStatus = FileStatus.InProgress;
        }

        public void DeleteFile(int fileId)
        {
            _fileRepository.Delete(fileId);
        }

        public bool IsFileAvailable(int fileId)
        {
            if (!_fileRepository.Table.Any(x => x.Id == fileId)) return false;
            return _fileRepository.GetById(fileId).FileStatus != FileStatus.InProgress;
        }

        public int GetNrNoLecturersPartims(string email)
        {
            return
                _counselorRepository.Table
                    .Where(x => x.Email == email)
                    .Select(x => x.Education)
                    .SelectMany(x => x.Routes)
                    .SelectMany(x => x.PartimInformation)
                    .Count(x => x.Lecturer.Email == "docent@howest.be");
        }

        public IQueryable<PartimInformation> GetPartimsNoLecturer(string email)
        {
            return _counselorRepository.Table
                .Where(x => x.Email == email)
                .Select(x => x.Education)
                .SelectMany(x => x.Routes)
                .SelectMany(x => x.PartimInformation)
                .Where(x => x.Lecturer.Email == "docent@howest.be")
                .ProjectTo<PartimInformation>();
        }

        public string[] AssignLector(string email, string superCode)
        {
            var errors = new List<string>();
            if(!email.Contains("@howest.be")) errors.Add("Emailadres niet van Howest");
            if(!_partimInformationRepository.Table.Any(x => x.SuperCode == superCode)) errors.Add("Verkeerde partim");
            if (errors.Count > 0) return errors.ToArray();
            
            _partimInformationRepository.Table.Where(x => x.SuperCode == superCode).Each(x => x.Lecturer = _lecturerRepository.Table.FirstOrDefault(l => l.Email == email) ?? new Entities.Lecturer
            {
                Email = email,
                InfoMail = DateTime.Now,
                WarningMail = DateTime.Now
            });
            errors.Add("Finish");
            return errors.ToArray();
        }

        public Models.File GetFileByFileId(int fileId)
        {
            var file = _fileRepository.GetById(fileId);

            var serviceFile = new Models.File
            {
                StudentFirstName = file.Student.FirstName,
                StudentName = file.Student.Name,
                StudentMail = file.Student.Email
            };

            foreach (var request in file.Requests)
            {
                foreach (var requestPartimInformation in request.RequestPartimInformations)
                {
                    var serviceModule = new Models.Module {Name = requestPartimInformation.PartimInformation.Module.Name};
                    var servicePartim = new Partim
                    {
                        Name = requestPartimInformation.PartimInformation.Partim.Name,
                        Evidence = request.Evidence.Select(e => new Evidence
                        {
                            Path = e.Path,
                            Description = e.Description,
                            Id = e.Id,
                            StudentEmail = e.Student.Email
                        }),
                        PrevEducations = request.PrevEducations.Select(e => new PrevEducation
                        {
                            Id = e.Id,
                            Education = e.Education
                        }),
                        FileId = request.FileId,
                        RequestId = request.Id,
                        Status = (Models.Status)requestPartimInformation.Status,
                        PartimInformationId = requestPartimInformation.PartimInformationId
                    };
                    serviceFile.InsertModule(serviceModule);
                    serviceFile.InsertPartim(servicePartim, serviceModule.Name);
                }
            }

            

            return serviceFile;
        } 

        public string GetEducationNameByCounselorEmail(string email)
        {

            if (!_counselorRepository.Table.Any(x => x.Email == email)) return "";
            return _counselorRepository.Table.First(s => s.Email == email)
                .Education.Name;
        }
        public IQueryable<Models.Education> GetEducations()
        {
            return _educationRepository.Table
                .ProjectTo<Models.Education>();
        }

        public void ChangeEducation(string email, string educationName)
        {
            var education = _educationRepository.Table.First(e => e.Name == educationName);
            var counselor = _counselorRepository.Table.First(c => c.Email == email);

            counselor.Education = education;
            _counselorRepository.Update(counselor);
        }

        public IQueryable<Models.File> GetFilesByCounselorEmail(string email, string academicYear)
        {
            if (!_counselorRepository.Table.Any())
                return new List<Models.File>().AsQueryable();

            var education = _counselorRepository
                .Table.First(t => t.Email == email)
                .Education;

            return 
                _fileRepository.Table.Where(
                    d =>
                        d.FileStatus != FileStatus.InProgress && d.AcademicYear == academicYear &&
                        d.Education.Id == education.Id)
                        .ProjectTo<Models.File>();
        }

        public FileView GetFile(int fileId)
        {
            var file = _fileRepository.GetById(fileId);
            var model = new FileView
            {
                MotivationList = _motivationRepository.Table.Where(x => x.Text != "geen"),
                Education = file.Education.Name,
                Counselor = file.Education.Counselors.First().Email ?? "none",
                DateCreated = file.DateCreated,
                AcademicYear = file.AcademicYear,
                Student = new StudentView { Email = file.Student.Email, Name = file.Student.Name, FirstName = file.Student.FirstName},
                Requests = file.Requests.Select(x => new RequestView
                {
                    Module = x.RequestPartimInformations.First().PartimInformation.Module.Name,
                    Partims = x.RequestPartimInformations.Select(r => new PartimView { Name = r.PartimInformation.Partim.Name, Status = (int)r.Status, Motivation = r.MotivationId}),
                    PrevEducationId  = x.PrevEducations.Select(e => e.Id),
                    EvidenceIds = x.Evidence.Select(e => e.Id)
                }),
                Evidence = file.Requests.SelectMany(x => x.Evidence).Distinct().Select(x => new EvidenceView
                {
                    Id = x.Id,
                    Path = x.Path,
                    Description = x.Description
                }),
                PrevEducations = file.Requests.SelectMany(x => x.PrevEducations).Distinct().Select(x => new PrevEducationView
                {
                    Id = x.Id,
                    Education = x.Education
                })
            };
            return model;
        }

        public void SendReminder(int aanvraagId)
        {
            IMailer mailer = new Mailer();
            var mail = mailer.ProduceMail();

            mail.To = _requestRepository.GetById(aanvraagId).File.Student.Email;

            mail.Body = "this is the body of the mail";

            mailer.SendMail(mail);
        }
    }
}