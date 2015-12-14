using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.ServiceLayer.Lecturer.Mappings;
using VTP2015.ServiceLayer.Lecturer.Models;
using System.Collections.Generic;

namespace VTP2015.ServiceLayer.Lecturer
{
    public class LecturerFacade : ILecturerFacade
    {
        private readonly IRepository<Entities.Lecturer> _lecturerRepository;
        private readonly IRepository<Entities.RequestPartimInformation> _requestPartimInformationRepository;
        private readonly IRepository<Entities.Motivation> _motivationRepository;

        public LecturerFacade(IUnitOfWork unitOfWork)
        {
            _lecturerRepository = unitOfWork.Repository<Entities.Lecturer>();
            _requestPartimInformationRepository = unitOfWork.Repository<Entities.RequestPartimInformation>();
            _motivationRepository = unitOfWork.Repository<Entities.Motivation>();

            var autoMaperConfig = new AutoMapperConfig();
            autoMaperConfig.Execute();
        }

        private IQueryable<RequestPartimInformation> GetRequestEntities(string email, Status status)
        {
            return _lecturerRepository.Table.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.RequestPartimInformations)
                .Where(a => a.Status == (Entities.Status)(int)status)
                .ProjectTo<RequestPartimInformation>();
        }

        public IQueryable<RequestPartimInformation> GetRequests(string email, Status status)
        {
            //var result = _lecturerRepository.Table.Where(b => b.Email == email)
            //    .SelectMany(p => p.PartimInformation)
            //    .SelectMany(p => p.RequestPartimInformations)
            //    .Where(a => (int)a.Status == (int)status).AsQueryable();
            //return result.ProjectTo<RequestPartimInformation>();

            var data = _lecturerRepository.Table.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.RequestPartimInformations)
                .Where(a => (int)a.Status == (int)status).AsQueryable();
            var result = new List<RequestPartimInformation>();
            foreach(var request in data){
                var student = request.Request.File.Student;
                result.Add(new RequestPartimInformation {
                    Id = request.Id,
                    Partim = new Partim {Code=request.PartimInformation.Partim.Code, Name=request.PartimInformation.Partim.Name },
                    Module = new Module { Code=request.PartimInformation.Module.Code, Name=request.PartimInformation.Module.Name},
                    Argumentation = request.Request.Argumentation,
                    Evidence = request.Request.Evidence.AsQueryable().ProjectTo<Evidence>(),
                    Status = (Status)(int)request.Status,
                    SuperCode = request.PartimInformation.SuperCode,
                    Student = new Models.Student { Id=student.Id.ToString(), Name=student.Name, FirstName=student.FirstName, Email=student.Email},
                    Motivation = new Motivation { ID=request.Motivation.Id, Text = request.Motivation.Text}
                });
            }
            return result.AsQueryable();
        }

        public IQueryable<Models.Student> GetUntreadedStudent(string email) //students
        {
            var result = _lecturerRepository.Table.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation).SelectMany(x => x.RequestPartimInformations)
                .Where(e => e.Status == Entities.Status.Untreated).Select(x => x.Request)
                .Select(f => f.File).Select(s => s.Student).Distinct().AsQueryable();

            return result.ProjectTo<Models.Student>();
        }

        public IQueryable<PartimInformation> GetPartims(string email) //partims
        {
            //var result = from requestPartimInformation in _lecturerRepository.Table.Where(b => b.Email == email)
            //    .SelectMany(p => p.PartimInformation)
            //    .SelectMany(p => p.RequestPartimInformations)
            //    .Where(a => a.Status == Entities.Status.Untreated)
            //             where requestPartimInformation.Id > 0
            //             group requestPartimInformation by requestPartimInformation.PartimInformation
            //                into groups
            //             select groups.FirstOrDefault();

            //return result.ProjectTo<RequestPartimInformation>();

            var result = _lecturerRepository.Table.Where(b => b.Email == email).SelectMany(p => p.PartimInformation).AsQueryable();
            return result.ProjectTo<PartimInformation>();

        }

        public bool Approve(int requestPartimInformationId, bool isApproved, string email, int motivation)
        {
            var requestPartimInformation = _requestPartimInformationRepository.GetById(requestPartimInformationId);

            if (requestPartimInformation.Status != (Entities.Status)(int)Status.Untreated || requestPartimInformation.PartimInformation.Lecturer.Email != email)
                return false;
            requestPartimInformation.MotivationId = motivation;
            requestPartimInformation.Status = isApproved ? (Entities.Status)(int)Status.Approved : (Entities.Status)(int)Status.Rejected;
            _requestPartimInformationRepository.Update(requestPartimInformation);
            
            return true;
        }

        public bool hasAny(string email, Status status)
        {
            return _lecturerRepository.Table.Where(x => x.Email==email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.RequestPartimInformations)
                .Where(a => a.Status == (Entities.Status)(int)status)
                .Any();
        }

        public IQueryable<Motivation> GetMotivations()
        {
            return _motivationRepository.Table.AsQueryable().ProjectTo<Motivation>();
        }
    }
}