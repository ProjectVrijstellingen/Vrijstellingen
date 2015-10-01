using System;
using System.Linq;
using VTP2015.DataAccess.ServiceRepositories;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Authentication
{
    public class AuthenticationFacade : IAuthenticationFacade
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IBamaflexRepository _bamaflexRepository;
        private readonly Repository<Entities.Counselor> _counselorRepository;
        private readonly Repository<Entities.Student> _studentRepository;
        private readonly Repository<Education> _educationRepository;

        public AuthenticationFacade(IUnitOfWork unitOfWork, IBamaflexRepository bamaflexRepository,
            IIdentityRepository identityRepository)
        {
            _counselorRepository = unitOfWork.Repository<Entities.Counselor>();
            _studentRepository = unitOfWork.Repository<Entities.Student>();
            _educationRepository = unitOfWork.Repository<Education>();
            _bamaflexRepository = bamaflexRepository;
            _identityRepository = identityRepository;
        }

        public bool IsCounselor(string email)
        {
            return _counselorRepository.Table.Any(c => c.Email == email);
        }

        public bool AuthenticateUserByEmail(string email, string password)
        {
            return _identityRepository.AuthenticateUserByEmail(email, password);
        }

        public void SyncStudentByUser(string email)
        {
            var user = _identityRepository.GetUserByEmail(email);
            var opleiding = _bamaflexRepository.GetEducationByStudentCode(user.Id);
                
            var student = _studentRepository.Table.FirstOrDefault(s => s.Email == email)
                          ?? new Entities.Student {Code = user.Id};

            var education = _educationRepository.Table.FirstOrDefault(e => e.Code == opleiding.Code)
                            ?? SyncEducations(opleiding.Code);

            student.Name = user.Lastname;
            student.FirstName = user.Firstname;
            student.Email = user.Email;
            student.PhoneNumber = user.ExtraInfo1;
            student.Education = education;

            if (student.Id > 0)
                _studentRepository.Update(student);
            else
                _studentRepository.Insert(student);
        }

        private Education SyncEducations(string educationCode)
        {
            var educations = _bamaflexRepository.GetEducations();

            Education returnValue = null;

            foreach (var model in educations.Select(education => new Education
            {
                AcademicYear = "2015-16", //Todo: ConfigFile gebruiken
                Code = education.Code,
                Name = education.Naam,
            }))
            {
                _educationRepository.Insert(model);
                if (model.Code == educationCode)
                    returnValue = model;
            }

            return returnValue;
        }
    }
}