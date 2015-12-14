using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.Bamaflex;
using VTP2015.DataAccess.ServiceRepositories;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Synchronisation
{
    class BamaflexSynchroniser : IBamaflexSynchroniser
    {
        private readonly IRepository<Entities.Student> _studentRepository;
        private readonly IRepository<Education> _educationRepository;
        private readonly IBamaflexRepository _bamaflexRepository;
        private readonly IRepository<PartimInformation> _partimInformationRepository;
        private readonly IRepository<Partim> _partimRepository;
        private readonly IRepository<Module> _moduleRepository;
        private readonly IRepository<Entities.Lecturer> _lectureRepository;
        private readonly IRepository<Route> _routeRepository;
        private readonly IIdentityRepository _identityRepository;

        public BamaflexSynchroniser(IRepository<Entities.Student> studentRepository,
            IRepository<Education> educationRepository, IBamaflexRepository bamaflexRepository,
            IRepository<PartimInformation> partimInformationRepository, IRepository<Partim> partimRepository,
            IRepository<Module> moduleRepository, IRepository<Entities.Lecturer> lectureRepository,
            IRepository<Route> routeRepository, IIdentityRepository identityRepository)
        {
            _studentRepository = studentRepository;
            _educationRepository = educationRepository;
            _bamaflexRepository = bamaflexRepository;
            _partimInformationRepository = partimInformationRepository;
            _partimRepository = partimRepository;
            _moduleRepository = moduleRepository;
            _lectureRepository = lectureRepository;
            _routeRepository = routeRepository;
            _identityRepository = identityRepository;
        }

        public bool SyncStudentPartims(string email, string academicYear)
        {
            if (StudentHasFilesInAcademicYear(email, academicYear))
                return false;

            var student = _studentRepository
                .Table.First(x => x.Email == email);

            if (StudentPartimsSynced(academicYear, student))
                return true;

            var education = _bamaflexRepository
                .GetEducation(student.Education);

            SyncEducations(education.Code, academicYear);

            foreach (var route in education.KeuzeTrajecten.Where(route => !_routeRepository.Table.Any(x => x.Name == route.Naam)))
            {
                _routeRepository.Insert(new Route
                {
                    Name = route.Naam,
                    Education = _educationRepository.GetById(student.Education.Id)
                });
            }
            if (!_routeRepository.Table.Where(x => x.EducationId == student.EducationId).Any(x => x.Name == "ModelRoute"))
                _routeRepository.Insert(new Route
                {
                    Name = "ModelRoute",
                    Education = _educationRepository.GetById(student.Education.Id)
                });
            StorePartimInfo(education.Modules.ToList(), student.EducationId, "ModelRoute");
            foreach (var route in education.KeuzeTrajecten) StorePartimInfo(route.Modules.ToList(), student.EducationId, route.Naam);
            
            return true;

        }

        public Education SyncEducations(string educationCode, string academicYear)
        {
            var educations = _bamaflexRepository.GetEducations();

            Education returnValue = null;

            foreach (var model in educations)
            {
                var education = _educationRepository.Table.FirstOrDefault(x => x.Code == model.Code);
                education.AcademicYear = academicYear;
                education.Code = model.Code;
                education.Name = model.Naam;
                _educationRepository.Update(education);
                if (education.Code == educationCode)
                    returnValue = education;
            }

            return returnValue;
        }

        public void SyncStudentByUser(string email, string academicYear)
        {
            var user = _identityRepository.GetUserByEmail(email);
            var opleiding = _bamaflexRepository.GetEducationByStudentCode(user.Id);

            var student = _studentRepository.Table.FirstOrDefault(s => s.Email == email)
                          ?? new Entities.Student { Code = user.Id };

            var education = _educationRepository.Table.FirstOrDefault(e => e.Code == opleiding.Code && e.AcademicYear == academicYear)
                            ?? SyncEducations(opleiding.Code, academicYear);

            student.Name = user.Lastname;
            student.FirstName = user.Firstname;
            student.Email = user.Email;
            student.PhoneNumber = "";
            student.Education = education;
            
            _studentRepository.Update(student);
        }

        private void StorePartimInfo(List<OpleidingsProgrammaOnderdeel> modules, int eductationId, string routeName)
        {
            foreach (var module in modules)
            {
                var moduleClass = _moduleRepository
                        .Table
                        .FirstOrDefault(m => m.Code == module.Code);
                moduleClass.Code = module.Code;
                moduleClass.Name = module.Naam;
                moduleClass.Semester = module.Semester;
                _moduleRepository.Update(moduleClass);

                foreach (var partim in module.Partims)
                {
                    var partimClass = new Partim();
                    
                    partimClass = _partimRepository
                        .Table
                        .FirstOrDefault(m => m.Code == partim.Code);
                    partimClass.Code = partim.Code;
                    partimClass.Name = partim.Naam;
                    _partimRepository.Update(partimClass);

                    var partimInfo =
                        _partimInformationRepository.Table.FirstOrDefault(x => x.SuperCode == partim.Supercode);
                    partimInfo.SuperCode = partim.Supercode;
                    partimInfo.Lecturer = partimInfo.Lecturer ?? _lectureRepository.Table.First(d => d.Email == "docent@howest.be");
                    partimInfo.Partim = partimClass;
                    partimInfo.Module = moduleClass;
                    partimInfo.Route =
                        _routeRepository.Table.Where(x => x.EducationId == eductationId).First(x => x.Name == routeName);
                    _partimInformationRepository.Update(partimInfo);
                }
            }
        }

        private bool StudentPartimsSynced(string academicYear, Entities.Student student)
        {
            return _educationRepository.GetById(student.Education.Id)
                .AcademicYear == academicYear; ;
        }

        private bool StudentHasFilesInAcademicYear(string email, string academicYear)
        {
            var studentHasFilesInAcademicYear = _studentRepository
                .Table
                .Where(s => s.Email == email)
                .SelectMany(s => s.Files)
                .Any(d => d.AcademicYear == academicYear);
            return studentHasFilesInAcademicYear;
        }
    }
}
