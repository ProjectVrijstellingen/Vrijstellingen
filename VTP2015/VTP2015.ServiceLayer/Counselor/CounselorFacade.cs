using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public CounselorFacade(IUnitOfWork unitOfWork)
        {
            _requestRepository = unitOfWork.Repository<Request>();
            _educationRepository = unitOfWork.Repository<Education>();
            _counselorRepository = unitOfWork.Repository<Entities.Counselor>();
            _fileRepository = unitOfWork.Repository<File>();
            _motivationRepository = unitOfWork.Repository<Motivation>();

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }

        public void RemovePartimFromFile(int partimInformationId)
        {
            var requestPartimInformation =
                _requestPartimInformationRepository.Table
                .First(r => r.PartimInformationId == partimInformationId);

            var request = requestPartimInformation.Request;
            _requestPartimInformationRepository.Delete(requestPartimInformation);

            if (request.RequestPartimInformations.Count < 1)
                _requestRepository.Delete(request);
        }

        public void SetFileStatusOpen(int fileId)
        {
            _fileRepository.GetById(fileId).FileStatus = FileStatus.InProgress;
        }

        public void DeleteFile(int fileId)
        {
            _fileRepository.Delete(fileId);
        }

        public Models.File GetFileByFileId(int fileId)
        {
            var file = _fileRepository.GetById(fileId);

            var serviceFile = new Models.File();
            serviceFile.StudentFirstName = file.Student.FirstName;
            serviceFile.StudentName = file.Student.Name;
            serviceFile.StudentMail = file.Student.Email;

            foreach (var request in file.Requests)
            {
                foreach (var partiminformation in request.RequestPartimInformations)
                {
                    var serviceModule = new Models.Module {Name = partiminformation.PartimInformation.Module.Name};
                    var servicePartim = new Partim
                    {
                        Name = partiminformation.PartimInformation.Partim.Name,
                        Evidence = request.Evidence.Select(e => new Evidence
                        {
                            Path = e.Path,
                            Description = e.Description,
                            EvidenceId = e.Id,
                            StudentEmail = e.Student.Email
                        }),
                        Argumentation = request.Argumentation,
                        FileId = request.FileId,
                        RequestId = request.Id,
                        Status = (Models.Status)partiminformation.Status,
                        PartimInformationId = partiminformation.Id
                    };
                    serviceFile.InsertModule(serviceModule);
                    serviceFile.InsertPartim(servicePartim, serviceModule.Name);
                }
            }

            return serviceFile;
        } 

        public IQueryable<Models.Request> GetRequests()
        {
            Debug.WriteLine("Started GetRequests");

            var starttime = DateTime.Now;

            var requests = _requestRepository.Table;

            var result = new List<Models.Request>();

            foreach (var request in requests)
            {
                var modules = new List<Models.Module>();
                foreach (var requestPartimInformation in request.RequestPartimInformations)
                {
                    var partimInformation = requestPartimInformation.PartimInformation;
                    if (modules.All(module => module.Name != partimInformation.Module.Name))
                        modules.Add(new Models.Module {Name = partimInformation.Module.Name, Partims = new List<Partim>()});

                    var partims = (List<Partim>) modules
                        .First(module => module.Name == partimInformation.Module.Name)
                        .Partims;

                    var partim = new Partim
                    {
                        Name = partimInformation.Partim.Name,
                        Evidence = request.Evidence.Select(evidence => new Evidence
                        {
                            Description = evidence.Description,
                            Path = evidence.Path,
                        }),
                        Status = (Models.Status) requestPartimInformation.Status
                    };

                    partims.Add(partim);

                }
                result.Add(new Models.Request { StudentName = request.File.Student.Name });
            }

            var endTime = DateTime.Now;

            Debug.WriteLine("time to finish algorithm: " + (endTime.Millisecond - starttime.Millisecond));

            return result.AsQueryable();
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
                MotivationList = _motivationRepository.Table,
                Education = file.Education.Name,
                Counselor = file.Education.Counselors.First().Email ?? "none",
                DateCreated = file.DateCreated,
                AcademicYear = file.AcademicYear,
                Student = new StudentView { Email = file.Student.Email, Name = file.Student.Name, FirstName = file.Student.FirstName},
                Requests = file.Requests.Select(x => new RequestView
                {
                    Module = x.RequestPartimInformations.First().PartimInformation.Module.Name,
                    Partims = x.RequestPartimInformations.Select(r => new PartimView { Name = r.PartimInformation.Partim.Name, Status = (int)r.Status, Motivation = r.MotivationId}),
                    Argumentation  = x.Argumentation ?? "",
                    EvidenceIds = x.Evidence.Select(e => e.Id)
                }),
                Evidence = file.Requests.SelectMany(x => x.Evidence).Distinct().Select(x => new EvidenceView
                {
                    Id = x.Id,
                    Path = x.Path,
                    Description = x.Description
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