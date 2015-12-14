using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions;
using VTP2015.DataAccess.ServiceRepositories;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Student.Mappings;
using VTP2015.ServiceLayer.Synchronisation;
using File = VTP2015.Entities.File;
using PartimMode = VTP2015.ServiceLayer.Student.Models.PartimMode;

namespace VTP2015.ServiceLayer.Student
{
    public class StudentFacade : IStudentFacade
    {
        private readonly IRepository<Entities.Student> _studentRepository;
        private readonly IRepository<Evidence> _evidenceRepository;
        private readonly IRepository<File> _fileRepository;
        private readonly IRepository<Request> _requestRepository; 
        private readonly IRepository<PartimInformation> _partimInformationRepository;
        private readonly IRepository<Education> _educationRepository;
        private readonly IRepository<RequestPartimInformation> _requestPartimInformationRepository;
        private readonly IBamaflexSynchroniser _synchroniser;
        private readonly IRepository<Motivation> _motivationRepository; 

        public StudentFacade(IUnitOfWork unitOfWork, IBamaflexRepository bamaflexRepository, IIdentityRepository identityRepository)
        {
            _studentRepository = unitOfWork.Repository<Entities.Student>();
            _evidenceRepository = unitOfWork.Repository<Evidence>();
            _fileRepository = unitOfWork.Repository<File>();
            _partimInformationRepository = unitOfWork.Repository<PartimInformation>();
            _requestRepository = unitOfWork.Repository<Request>();
            _educationRepository = unitOfWork.Repository<Education>();
            var routeRepository = unitOfWork.Repository<Route>();
            var lectureRepository = unitOfWork.Repository<Entities.Lecturer>();
            var partimRepository = unitOfWork.Repository<Partim>();
            var moduleRepository = unitOfWork.Repository<Module>();
            _requestPartimInformationRepository = unitOfWork.Repository<RequestPartimInformation>();
            _motivationRepository = unitOfWork.Repository<Motivation>();

            _synchroniser = new BamaflexSynchroniser(_studentRepository, _educationRepository,
                bamaflexRepository, _partimInformationRepository, partimRepository, moduleRepository,
                lectureRepository, routeRepository, identityRepository);
            var automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute();
        }

        public string GetStudentCodeByEmail(string email)
        {
            return _studentRepository.Table
                .First(user => user.Email == email).Code;
        }

        public void InsertEvidence(Models.Evidence evidence, string studentMail)
        {
            var entity = new Evidence
            {
                Description = evidence.Description,
                Path = evidence.Path,
                Student = _studentRepository.Table.First(s => s.Email == studentMail)
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

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _evidenceRepository.Delete(evidence);

            return true;
        }

        public IQueryable<Models.File> GetFilesByStudentEmail(string email)
        {
            return _fileRepository
                .Table.Where(f => f.Student.Email == email).ProjectTo<Models.File>();
        }

        public IQueryable<Models.Evidence> GetEvidenceByStudentEmail(string email)
        {
            return _evidenceRepository.Table
                .Where(e => e.Student.Email == email)
                .ProjectTo<Models.Evidence>();
        }

        public bool IsFileFromStudent(string email, int fileId)
        {
            return _fileRepository.Table
                .Where(f => f.Id == fileId)
                .Any(d => d.Student.Email == email);
        }

        public IQueryable<Models.PartimInformation> GetPartims(int fileId, PartimMode partimMode)
        {
            var requestedPartims = _requestRepository.Table
                .Where(a => a.FileId == fileId)
                .SelectMany(a => a.RequestPartimInformations);

            switch (partimMode)
            { 
                case PartimMode.Requested:
                    return requestedPartims.ProjectTo<Models.PartimInformation>();
                case PartimMode.Available:
                    return
                        _fileRepository.Table.Where(s => s.Id == fileId)
                            .Select(s => s.Education)
                            .SelectMany(e => e.Routes)
                            .SelectMany(r => r.PartimInformation)
                            .Except(requestedPartims.Select(x => x.PartimInformation))
                            .ProjectTo<Models.PartimInformation>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(partimMode), partimMode, null);
            }
        }

        public IQueryable<Models.Request> GetRequestByFileId(int fileId)
        {
            var requests = _requestRepository.Table.Where(request => request.FileId == fileId);
            var requestList = requests.Where(request => request.RequestPartimInformations.All(x => x.Status == Status.Empty))
                .Select(request => new Models.Request
                {
                    FileId = request.FileId,
                    Id = request.Id,
                    ModuleName = request.RequestPartimInformations.FirstOrDefault().PartimInformation.Module.Name,
                    PartimName =
                        request.RequestPartimInformations.Count ==
                        request.RequestPartimInformations.FirstOrDefault()
                            .PartimInformation.Module.PartimInformation.Count
                            ? ""
                            : request.RequestPartimInformations.FirstOrDefault().PartimInformation.Partim.Name,
                    Code =
                        request.RequestPartimInformations.Count ==
                        request.RequestPartimInformations.FirstOrDefault()
                            .PartimInformation.Module.PartimInformation.Count
                            ? request.RequestPartimInformations.FirstOrDefault().PartimInformation.Module.Code
                            : request.RequestPartimInformations.FirstOrDefault().PartimInformation.SuperCode,
                    Argumentation = request.Argumentation,
                    Evidence =
                        request.Evidence.Select(
                            x => new Models.Evidence {Id = x.Id, Description = x.Description, Path = x.Path})
                            .AsQueryable(),
                    Submitted = false,
                    Motivation = request.RequestPartimInformations.FirstOrDefault().Motivation.Text
                }).ToList();
            requests.Where(request => request.RequestPartimInformations.All(x => x.Status != Status.Empty))
                .Each(request => requestList.AddRange(request.RequestPartimInformations.Select(partiminfo => new Models.Request
                {
                    FileId = request.FileId,
                    Id = request.Id,
                    ModuleName = partiminfo.PartimInformation.Module.Name,
                    PartimName = partiminfo.PartimInformation.Partim.Name,
                    Code = partiminfo.PartimInformation.SuperCode,
                    Argumentation = request.Argumentation,
                    Evidence =
                        request.Evidence.Select(
                            x => new Models.Evidence { Id = x.Id, Description = x.Description, Path = x.Path })
                            .AsQueryable(),
                    Submitted = true,
                    Motivation = partiminfo.Motivation.Text
                })));
            return requestList.AsQueryable();
        }

        public bool SyncStudentPartims(string email, string academicYear)
        {
            return _synchroniser.SyncStudentPartims(email, academicYear);
        }

        public void SyncStudent(string email, string academicYear)
        {
            _synchroniser.SyncStudentByUser(email, academicYear);
        }

        public Models.Evidence GetEvidenceById(int evidenceId)
        {
            var entity = _evidenceRepository.Table.First(e => e.Id == evidenceId);

            return Mapper.Map<Models.Evidence>(entity);
        }

        public int InsertFile(Models.File file)
        {
            var entity = new File
            {
                AcademicYear = file.AcademicYear,
                DateCreated = file.DateCreated,
                FileStatus = FileStatus.InProgress,
                Education = _educationRepository.Table.First(education => education.Name == file.Education),
                Student = _studentRepository.Table.First(student => student.Email == file.StudentMail)
            };

            _fileRepository.Insert(entity);
            return entity.Id;
        }

        public string AddRequestInFile(int fileId, string code)
        {
            if (!_fileRepository.Table.Any(d => d.Id == fileId)) return "";
            int fakeint;
            var isSuperCode = int.TryParse(code.Substring(0, 1), out fakeint);
            var requestedPartims = _requestPartimInformationRepository.Table.Where(x => x.Request.FileId == fileId);
            if (isSuperCode
                ? requestedPartims.Any(x => x.PartimInformation.SuperCode == code)
                : (requestedPartims.Any(x => x.PartimInformation.Module.Code == code) ||
                   requestedPartims.Any(
                       x =>
                           _partimInformationRepository.Table.Where(p => p.Module.Code == code)
                               .Any(p => p == x.PartimInformation))))
                return "Fake!";
            if (_fileRepository.GetById(fileId).FileStatus == FileStatus.Submitted) return "Denied!";

            var newRequest = new Request {LastChanged = DateTime.Now};
            _fileRepository.GetById(fileId).Requests.Add(newRequest);
            newRequest.Name = "request " + newRequest.Id;

            if (isSuperCode)
            {
                _requestPartimInformationRepository.Insert(new RequestPartimInformation
                {
                    Status = Status.Empty,
                    Motivation = _motivationRepository.GetById(1),
                    PartimInformation = _partimInformationRepository.Table.First(x => x.SuperCode == code),
                    Request = newRequest
                });
            }
            else
            {
                _partimInformationRepository.Table.Where(x => x.Module.Code == code).ToList()
                    .ForEach(partimInfo => _requestPartimInformationRepository.Insert(new RequestPartimInformation
                    {
                        Status = Status.Empty,
                        Motivation = _motivationRepository.GetById(1),
                        PartimInformation = partimInfo,
                        Request = newRequest
                    }));
            }
            return newRequest.Id.ToString();
        }

        public void SumbitFile(int fileId)
        {
            var file = _fileRepository.GetById(fileId);
            if (file.Requests.Count < 1) return;
            if(file.FileStatus == FileStatus.InProgress) file.DateCreated = DateTime.Now;
            file.FileStatus = FileStatus.Submitted;
            foreach (var partiminfo in file.Requests.SelectMany(request => request.RequestPartimInformations.Where(x => x.Status == Status.Empty)))
            {
                if (partiminfo.Request.Evidence.Count < 1)
                {
                    partiminfo.Status = Status.Rejected;
                    partiminfo.Motivation = _motivationRepository.GetById(6);
                }
                else
                {
                    partiminfo.Status = Status.Untreated;
                }
            }
            _fileRepository.Update(file);
        }

        public Models.FileStatus GetFileStatus(int fileId)
        {
            return (Models.FileStatus) (int) _fileRepository.GetById(fileId).FileStatus;
        }

        public IQueryable<Models.Student> GetStudent(string email)
        {
            return _studentRepository.Table.Where(x => x.Email == email).ProjectTo<Models.Student>();
        }

        public bool SyncRequestInFile(Models.Request requestModel)
        {
            var request = _requestRepository.GetById(requestModel.Id);
            if (request.RequestPartimInformations.Any(x => x.Status != Status.Empty)) return false;
            request.Argumentation = requestModel.Argumentation;
            request.LastChanged = DateTime.Now;
            _requestRepository.Update(request);
            if (requestModel.Evidence != null)
            {
                var evidence = requestModel.Evidence.Select(e => _evidenceRepository.GetById(e.Id))
                    .Where(e => e.Student.Files.Last().Id == requestModel.FileId);

                request.Evidence.Except(evidence).ToList().ForEach(e => request.Evidence.Remove(e));
                evidence.Except(request.Evidence).ToList().ForEach(e => request.Evidence.Add(e));
            }
            _requestRepository.Update(request);
            return true;
        }

        public bool DeleteRequest(int fileId, int requestId)
        {
            if (!_requestRepository.Table.Any(d => d.FileId == fileId && d.Id == requestId))
                return false;

            var request = _requestRepository.GetById(requestId);
            _requestRepository.Delete(request);

            return true;
        }

        public Education GetEducation(string studentMail)
        {
            return _studentRepository.Table.First(s => s.Email == studentMail).Education;
        }
    }
}