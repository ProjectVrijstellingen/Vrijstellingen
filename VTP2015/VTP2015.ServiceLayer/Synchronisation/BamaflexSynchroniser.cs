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

        public BamaflexSynchroniser(IRepository<Entities.Student> studentRepository,
            IRepository<Education> educationRepository, IBamaflexRepository bamaflexRepository,
            IRepository<PartimInformation> partimInformationRepository, IRepository<Partim> partimRepository,
            IRepository<Module> moduleRepository, IRepository<Entities.Lecturer> lectureRepository,
            IRepository<Route> routeRepository)
        {
            _studentRepository = studentRepository;
            _educationRepository = educationRepository;
            _bamaflexRepository = bamaflexRepository;
            _partimInformationRepository = partimInformationRepository;
            _partimRepository = partimRepository;
            _moduleRepository = moduleRepository;
            _lectureRepository = lectureRepository;
            _routeRepository = routeRepository;
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

            foreach (var route in education.KeuzeTrajecten)
            {
                if (!_routeRepository.Table.Any(x => x.Name == route.Naam)) _routeRepository.Insert(new Route
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

        private void StorePartimInfo(List<OpleidingsProgrammaOnderdeel> modules, int eductationId, string RouteName)
        {
            foreach (var module in modules)
            {
                if (!_moduleRepository.Table.Any(x => x.Code == module.Code))
                    _moduleRepository.Insert(new Module
                    {
                        Code = module.Code,
                        Name = module.Naam
                    });
                var moduleClass = _moduleRepository
                        .Table
                        .First(m => m.Code == module.Code);
                foreach (var partim in module.Partims)
                {
                    if (!_partimRepository.Table.Any(x => x.Code == partim.Code))
                        _partimRepository.Insert(new Partim
                        {
                            Code = partim.Code,
                            Name = partim.Naam
                        });
                    var partimClass = _partimRepository
                        .Table
                        .First(m => m.Code == partim.Code);

                    if (!_partimInformationRepository.Table.Any(x => x.SuperCode == partim.Supercode))
                    {
                        var partimInfo = new PartimInformation
                        {
                            SuperCode = partim.Supercode,
                            Lecturer = _lectureRepository.Table.First(d => d.Email == "docent@howest.be"),
                            Partim = partimClass,
                            Module = moduleClass,
                            Route = _routeRepository.Table.Where(x => x.EducationId == eductationId).First(x => x.Name == RouteName)
                        };
                        _partimInformationRepository.Insert(partimInfo);
                    }
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
