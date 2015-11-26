using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Counselor.Mappings;
using VTP2015.ServiceLayer.Mail;
using Status = VTP2015.ServiceLayer.Counselor.Models.Status;

namespace VTP2015.ServiceLayer.Counselor
{
    public class CounselorFacade : ICounselorFacade
    {
        private readonly Repository<Request> _requestRepository;
        private readonly Repository<Education> _educationRepository;
        private readonly Repository<Entities.Counselor> _counselorRepository;
        private readonly Repository<File> _fileRepository;
        private readonly Repository<RequestPartimInformation> _requestPartimInformationRepository; 

        public CounselorFacade(IUnitOfWork unitOfWork)
        {
            _requestRepository = unitOfWork.Repository<Request>();
            _educationRepository = unitOfWork.Repository<Education>();
            _counselorRepository = unitOfWork.Repository<Entities.Counselor>();
            _fileRepository = unitOfWork.Repository<File>();
            _requestPartimInformationRepository = unitOfWork.Repository<RequestPartimInformation>();

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }

        public IQueryable<Models.Request> GetRequests()
        {
            return _requestPartimInformationRepository.Table
                .Select(requestPartimInformation => new Models.Request
                {
                    Argumentation = requestPartimInformation.Request.Argumentation,
                    FileId = requestPartimInformation.Request.FileId,
                    Evidence = requestPartimInformation.Request.Evidence
                        .Select(e => new Models.Evidence
                        {
                            Description = e.Description,
                            EvidenceId = e.Id,
                            Path = e.Path,
                            StudentEmail = e.Student.Email
                        }).AsQueryable(),
                    ModuleName = requestPartimInformation.PartimInformation.Module.Name,
                    PartimName = requestPartimInformation.PartimInformation.Partim.Name,
                    RequestId = requestPartimInformation.RequestId,
                    Status = (Status) requestPartimInformation.Status
                });
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

        public IQueryable<Models.File> GetFileByCounselorEmail(string email, string academicYear)
        {
            if (!_counselorRepository.Table.Any())
                return new List<Models.File>().AsQueryable();

            var education = _counselorRepository
                .Table.First(t => t.Email == email)
                .Education;

            return _fileRepository.Table.Where(d => d.FileStatus != FileStatus.InProgress && d.AcademicYear == academicYear && d.Education.Id == education.Id)
                .ProjectTo<Models.File>();
        }

        public void SendReminder(int aanvraagId)
        {
            IMailer mailer = new Mailer();
            var mail = mailer.ProduceMail();

            mail.To = _requestRepository.GetById(aanvraagId).File.Student.Email;

            mail.Body = "this is the body of the mail, now fuck off";

            mailer.SendMail(mail);
        }
    }
}