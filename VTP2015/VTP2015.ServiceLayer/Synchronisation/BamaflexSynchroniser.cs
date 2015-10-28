using System.Linq;
using VTP2015.DataAccess.ServiceRepositories;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Synchronisation
{
    class BamaflexSynchroniser : IBamaflexSynchroniser
    {
        private readonly Repository<Entities.Student> _studentRepository;
        private readonly Repository<Education> _educationRepository;
        private readonly IBamaflexRepository _bamaflexRepository;
        private readonly Repository<PartimInformation> _partimInformationRepository;
        private readonly Repository<Partim> _partimRepository;
        private readonly Repository<Module> _moduleRepository;
        private readonly Repository<Entities.Lecturer> _lectureRepository;
        private readonly Repository<Route> _routeRepository;

        public BamaflexSynchroniser(Repository<Entities.Student> studentRepository,
            Repository<Education> educationRepository, IBamaflexRepository bamaflexRepository,
            Repository<PartimInformation> partimInformationRepository, Repository<Partim> partimRepository,
            Repository<Module> moduleRepository, Repository<Entities.Lecturer> lectureRepository,
            Repository<Route> routeRepository)
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

            if (IsStudentEducationFromCurrentAcademicYear(academicYear, student))
                return true;

            var routes = _bamaflexRepository
                .GetRoutes(student.Education);

            foreach (var route in routes)
            {
                var localRoute = new Route
                {
                    Name = route.Naam,
                    Education = _educationRepository.GetById(student.Education.Id)
                };

                var supercodes = route
                    .Modules
                    .SelectMany(m => m.Partims)
                    .Select(p => p.Supercode);

                foreach (var supercode in supercodes)
                {
                    if (_partimInformationRepository.Table.Any(x => x.SuperCode == supercode))
                        continue;

                    var partimInformation = _bamaflexRepository
                        .GetPartimInformationBySupercode(supercode);

                    var partimInfo = new PartimInformation
                    {
                        SuperCode = partimInformation.Supercode.Supercode1,
                        Lecturer = _lectureRepository.Table.First(d => d.Email == "docent@howest.be"),
                        Partim = _partimRepository
                            .Table
                            .First(m => m.Code == partimInformation.Partim.Id)
                                 ?? new Partim
                                 {
                                     Code = partimInformation.Partim.Id,
                                     Name = partimInformation.Partim.Naam
                                 },
                        Module = _moduleRepository
                            .Table
                            .First(m => m.Code == partimInformation.Module.Id)
                                 ?? new Module
                                 {
                                     Code = partimInformation.Module.Id,
                                     Name = partimInformation.Module.Naam
                                 }
                    };

                    localRoute.PartimInformation.Add(partimInfo);
                }

                _routeRepository.Insert(localRoute);
            }


            return true;

        }

        private bool IsStudentEducationFromCurrentAcademicYear(string academicYear, Entities.Student student)
        {
            var isStudentEducationFromCurrentAcademicYear = _educationRepository
                .GetById(student.Education.Id)
                .AcademicYear == academicYear;
            return isStudentEducationFromCurrentAcademicYear;
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
