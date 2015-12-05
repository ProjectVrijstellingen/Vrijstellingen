using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
using Status = VTP2015.ServiceLayer.Counselor.Models.Status;

namespace VTP2015.ServiceLayer.Counselor
{
    public class CounselorFacade : ICounselorFacade
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<Education> _educationRepository;
        private readonly IRepository<Entities.Counselor> _counselorRepository;
        private readonly IRepository<File> _fileRepository;

        public CounselorFacade(IUnitOfWork unitOfWork)
        {
            _requestRepository = unitOfWork.Repository<Request>();
            _educationRepository = unitOfWork.Repository<Education>();
            _counselorRepository = unitOfWork.Repository<Entities.Counselor>();
            _fileRepository = unitOfWork.Repository<File>();

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute();
        }

        public IQueryable<Models.Request> GetRequests()
        {
            var requests = _requestRepository.Table;

            var result = new List<Models.Request>();

            foreach (var request in requests)
            {
                var modules = new List<Models.Module>();
                foreach (var partimInformation in request.RequestPartimInformations.Select(requestPartimInformation => requestPartimInformation.PartimInformation))
                {
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
                            Path = evidence.Path
                        })
                    };

                    partims.Add(partim);
                }
                result.Add(new Models.Request { StudentName = request.File.Student.Name, Modules = modules});
            }

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