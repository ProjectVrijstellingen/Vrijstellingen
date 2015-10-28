using System;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using VTP2015.DataAccess.ServiceRepositories;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Student.Mappings;
using VTP2015.ServiceLayer.Synchronisation;
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

        public bool IsRequestFromStudent(int fileId, int requestId, string email)
        {
            return _studentRepository.Table.Where(s => s.Email == email)
                    .SelectMany(s => s.Files)
                    .Where(d => d.Id == fileId)
                    .SelectMany(d => d.Requests)
                    .Any(r => r.Id == requestId);
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
                .SelectMany(a => a.RequestPartimInformations)
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
                .Where(request => request.FileId == fileId)
                .Select(request => new Models.Request
                {
                    Argumentation = request.Argumentation,
                    FileId = request.FileId,
                    LastChanged = request.LastChanged,
                    PartimInformationIds = request.RequestPartimInformations.Select(x => x.PartimInformationId).AsQueryable(),
                    Evidence = request.Evidence.Select(evidence => new Models.Evidence
                    {
                        Description = evidence.Description,
                        Id = evidence.Id,
                        Path = evidence.Path,
                        StudentMail = evidence.Student.Email
                    }).AsQueryable()
                });
        }

        public bool SyncStudentPartims(string email, string academicYear)
        {
            IBamaflexSynchroniser synchroniser = new BamaflexSynchroniser(_studentRepository, _educationRepository,
                _bamaflexRepository, _partimInformationRepository, _partimRepository, _moduleRepository,
                _lectureRepository, _routeRepository);

            return synchroniser.SyncStudentPartims(email, academicYear);
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
                AcademicYear = file.AcademicYear,
                DateCreated = file.DateCreated,
                Editable = file.Editable,
                Education = _educationRepository.Table.First(education => education.Name == file.Education),
                Student = _studentRepository.Table.First(student => student.Email == file.StudentMail)
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

        public bool DeleteRequest(int fileId, int requestId)
        {
            if (!_requestRepository.Table.Any(d => d.Id == fileId && d.Id == requestId))
                return false;

            var request = _requestRepository.Table.First(d => d.Id == fileId && d.Id == requestId);
            _requestRepository.Delete(request);

            return true;
        }

        public Education GetEducation(string studentMail)
        {
            return _studentRepository.Table.First(s => s.Email == studentMail).Education;
        }
    }
}