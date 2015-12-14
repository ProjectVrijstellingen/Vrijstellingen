using System;
using System.Diagnostics;
using System.Linq;
using VTP2015.DataAccess.ServiceRepositories;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Synchronisation;

namespace VTP2015.ServiceLayer.Authentication
{
    public class AuthenticationFacade : IAuthenticationFacade
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IBamaflexRepository _bamaflexRepository;
        private readonly IRepository<Entities.Counselor> _counselorRepository;
        private readonly IRepository<Entities.Student> _studentRepository;
        private readonly IRepository<PartimInformation> _partimInformationRepository;
        private readonly IRepository<Entities.Lecturer> _lectureRepository;
        private readonly IRepository<Partim> _partimRepository;
        private readonly IRepository<Module> _moduleRepository;
        private readonly IRepository<Education> _educationRepository;
        private readonly IRepository<Route> _routeRepository;  

        public AuthenticationFacade(IUnitOfWork unitOfWork, IBamaflexRepository bamaflexRepository,
            IIdentityRepository identityRepository)
        {
            _counselorRepository = unitOfWork.Repository<Entities.Counselor>();
            _studentRepository = unitOfWork.Repository<Entities.Student>();
            _educationRepository = unitOfWork.Repository<Education>();
            _bamaflexRepository = bamaflexRepository;
            _identityRepository = identityRepository;
            _lectureRepository = unitOfWork.Repository<Entities.Lecturer>();
            _partimInformationRepository = unitOfWork.Repository<PartimInformation>();
            _routeRepository = unitOfWork.Repository<Route>();
            _partimRepository = unitOfWork.Repository<Partim>();
            _moduleRepository = unitOfWork.Repository<Module>();
        }

        public bool IsCounselor(string email)
        {
            return _counselorRepository.Table.Any(c => c.Email == email);
        }

        public bool AuthenticateUserByEmail(string email, string password)
        {
            return _identityRepository.AuthenticateUserByEmail(email, password);
        }

        public void SyncStudentByUser(string email, string academicYear)
        {
            IBamaflexSynchroniser synchroniser = new BamaflexSynchroniser(_studentRepository, _educationRepository,
                _bamaflexRepository, _partimInformationRepository, _partimRepository, _moduleRepository,
                _lectureRepository, _routeRepository, _identityRepository);

            synchroniser.SyncStudentByUser(email, academicYear);
        }

        public void SyncLecturer(string email)
        {
            if (_lectureRepository.Table.Any(x => x.Email == email)) return;
            var lecturer = new Entities.Lecturer
            {
                Email = email,
                InfoMail = DateTime.Now,
                WarningMail = DateTime.Now
            };
            _lectureRepository.Insert(lecturer);
        }
    }
}