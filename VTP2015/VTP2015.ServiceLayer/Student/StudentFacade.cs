using System;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using VTP2015.DataAccess.ServiceRepositories;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Student.Mappings;
using File = VTP2015.Entities.File;

namespace VTP2015.ServiceLayer.Student
{
    public class StudentFacade : IStudentFacade
    {
        private readonly IBamaflexRepository _bamaflexRepository;
        private readonly Repository<Entities.Student> _studentRepository;
        private readonly Repository<Evidence> _evidenceRepository;
        private readonly Repository<File> _fileRepository;
        private readonly Repository<Request> _requestRepository; 
        private readonly Repository<PartimInformation> _partimInformationRepository;
        private readonly Repository<Education> _educationRepository;
        private readonly Repository<Route> _routeRepository;
        private readonly Repository<Entities.Lecturer> _lectureRepository;
        private readonly Repository<Partim> _partimRepository;
        private readonly Repository<Module> _moduleRepository; 

        public StudentFacade(IUnitOfWork unitOfWork, IBamaflexRepository bamaflexRepository)
        {
            _bamaflexRepository = bamaflexRepository;

            _studentRepository = unitOfWork.Repository<Entities.Student>();
            _evidenceRepository = unitOfWork.Repository<Evidence>();
            _fileRepository = unitOfWork.Repository<File>();
            _partimInformationRepository = unitOfWork.Repository<PartimInformation>();
            _requestRepository = unitOfWork.Repository<Request>();
            _educationRepository = unitOfWork.Repository<Education>();
            _routeRepository = unitOfWork.Repository<Route>();
            _lectureRepository = unitOfWork.Repository<Entities.Lecturer>();
            _partimRepository = unitOfWork.Repository<Partim>();
            _moduleRepository = unitOfWork.Repository<Module>();

            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute();
        }

        public string GetStudentCodeByEmail(string email)
        {
            return _studentRepository.Table
                .First(user => user.Email == email).Code;
        }

        public void InsertEvidence(Models.Evidence evidence)
        {
            var entity = new Evidence
            {
                Description = evidence.Description,
                Path = evidence.Path,
                Student = _studentRepository.Table.First(s => s.Email == evidence.StudentMail)
            };
            _evidenceRepository.Insert(entity);
        }

        public bool IsEvidenceFromStudent(string email)
        {
            return _evidenceRepository.Table.Any(b => b.Student.Email == email);
        }

        public bool IsRequestFromStudent(int fileId, string supercode, string email)
        {
            return
                _studentRepository.Table.Where(s => s.Email == email)
                    .Select(s => s.Education)
                    .SelectMany(e => e.Routes)
                    .SelectMany(r => r.PartimInformation)
                    .Any(p => p.SuperCode == supercode) &&
                _studentRepository.Table.Where(s => s.Email == email)
                    .SelectMany(s => s.Files)
                    .Any(d => d.Id == fileId);
        }

        public bool DeleteEvidence(int evidenceId, string mapPath)
        {
            var evidence = _evidenceRepository.GetById(evidenceId);

            var path = Path.Combine(mapPath, evidence.Path);

            if (!System.IO.File.Exists(path))
                return false;

            System.IO.File.Delete(path);

            _evidenceRepository.Delete(evidence);

            return true;
        }

        public IQueryable<Models.File> GetFilesByStudentEmail(string email)
        {
            return _fileRepository
                .Table.Where(f => f.Student.Email == email)
                .Project().To<Models.File>();
        }

        public IQueryable<Models.Evidence> GetEvidenceByStudentEmail(string email)
        {
            return _evidenceRepository.Table
                .Where(e => e.Student.Email == email)
                .Project().To<Models.Evidence>();
        }

        public bool IsFileFromStudent(string email, int fileId)
        {
            return _fileRepository.Table
                .Where(f => f.Id == fileId)
                .Any(d => d.Student.Email == email);
        }

        public IQueryable<Models.PartimInformation> GetPartims(string email, int fileId, PartimMode partimMode)
        {
            var requestedPartims = _requestRepository.Table
                .Where(a => a.Id == fileId && a.File.Student.Email == email)
                .Select(a => a.PartimInformation);

            switch (partimMode)
            { 
                case PartimMode.Requested:
                    return requestedPartims.Project().To<Models.PartimInformation>();
                case PartimMode.Available:
                    return
                        _studentRepository.Table.Where(s => s.Email == email)
                            .Select(s => s.Education)
                            .SelectMany(e => e.Routes)
                            .SelectMany(r => r.PartimInformation)
                            .Except(requestedPartims)
                            .Project()
                            .To<Models.PartimInformation>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(partimMode), partimMode, null);
            }
        }

        public IQueryable<Models.Request> GetRequestsByFileId(int fileId)
        {
            return _requestRepository.Table
                .Where(r => r.Id == fileId)
                .Project()
                .To<Models.Request>();
        }

        public bool SyncStudentPartims(string email, string academicYear)
        {
            if (_studentRepository.Table.Where(s => s.Email == email).SelectMany(s => s.Files).Any(d => d.AcademicYear == academicYear)) return false;
            var student = _studentRepository.Table.First(x => x.Email == email);
            if (_educationRepository.GetById(student.Education.Id).AcademicYear == academicYear) return true;

            var routes = _bamaflexRepository.GetRoutes(student.Education);
            foreach (var route in routes)
            {
                var localRoute = new Route
                {
                    Name = route.Naam
                };
                _routeRepository.Insert(localRoute);
                _educationRepository.GetById(student.Education.Id).Routes.Add(localRoute);
                foreach (var supercode in route.Modules.SelectMany(x => x.Partims).Select(x => x.Supercode))
                {
                    if (_partimInformationRepository.Table.Any(x => x.SuperCode == supercode)) continue;
                    var partimInformation = _bamaflexRepository.GetPartimInformationBySupercode(supercode);
                    var partimInfo = new PartimInformation
                    {
                        SuperCode = partimInformation.Supercode.Supercode1, Lecturer = _lectureRepository.Table.First(d => d.Email == "docent@howest.be") //Needs real input!!!
                    };

                    if (_partimRepository.Table.Any(p => p.Code == partimInformation.Partim.Id))
                    {
                        partimInfo.Partim = _partimRepository.Table.First(m => m.Code == partimInformation.Partim.Id);
                    }
                    else
                    {
                        partimInfo.Partim = new Partim
                        {
                            Code = partimInformation.Partim.Id, Name = partimInformation.Partim.Naam
                        };
                    }

                    if (_moduleRepository.Table.Any(m => m.Code == partimInformation.Module.Id))
                    {
                        partimInfo.Module = _moduleRepository.Table.First(m => m.Code == partimInformation.Module.Id);
                    }
                    else
                    {
                        partimInfo.Module = new Module
                        {
                            Code = partimInformation.Module.Id, Name = partimInformation.Module.Naam
                        };
                    }
                    localRoute.PartimInformation.Add(partimInfo);
                    _partimInformationRepository.Insert(partimInfo);
                }
            }
            return true;
        }

        public Models.Evidence GetEvidenceById(int evidenceId)
        {
            var entity = _evidenceRepository.Table.First(e => e.Id == evidenceId);

            return Mapper.Map<Models.Evidence>(entity);
        }

        public Models.PartimInformation GetPartimInformationBySuperCode(string superCode)
        {
            var entity = _partimInformationRepository.Table.First(p => p.SuperCode == superCode);

            return Mapper.Map<Models.PartimInformation>(entity);
        }

        public int InsertFile(Models.File file)
        {
            var entity = new File
            {
                AcademicYear = file.AcademicYear, DateCreated = file.DateCreated, Editable = file.Editable, Specialization = file.Specialization, Student = _studentRepository.Table.First(s => s.Email == file.StudentMail)
            };

            _fileRepository.Insert(entity);
            return entity.Id;
        }

        public bool SyncRequestInFile(Models.Request request)
        {
            //Todo:hermaken
            //if (
            //    _requestRepository.Table.Any(
            //        x => x.FileId == request.FileId && x.SuperCode == request.PartimInformation.SuperCode))
            //{
            //    var bestaandeAanvraag = _requestRepository.Table.First(
            //        x => x.FileId == request.FileId && x.SuperCode == request.PartimInformation.SuperCode);

            //    bestaandeAanvraag.LastChanged = request.LastChanged;
            //    bestaandeAanvraag.Argumentation = request.Argumentation;

            //    var verwijderdeBewijzen = bestaandeAanvraag.Evidence.Except(request.Evidence).ToList();
            //    var toegevoegdeBewijzen = request.Evidence.Except(bestaandeAanvraag.Evidence).ToList();

            //    verwijderdeBewijzen.ForEach(b => bestaandeAanvraag.Evidence.Remove(b));

            //    foreach (var b in toegevoegdeBewijzen)
            //    {
            //        if (_db.Context.Entry(b).State == EntityState.Detached)
            //        {
            //            _db.Context.Evidence.Attach(b);
            //        }
            //        bestaandeAanvraag.Evidence.Add(b);
            //    }
            //    _genericRepository.Update(bestaandeAanvraag);
            //}
            //else
            //{
            //    _genericRepository.Insert(request);
            //}
            return true;
        }

        public bool DeleteRequest(int fileId, string supercode)
        {
            if (!_requestRepository.Table.Any(d => d.Id == fileId && d.PartimInformation.SuperCode == supercode))
                return false;

            var request = _requestRepository.Table.First(d => d.Id == fileId && d.PartimInformation.SuperCode == supercode);
            _requestRepository.Delete(request);

            return true;
        }
    }
}