using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.ServiceLayer.Lecturer.Mappings;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.ServiceLayer.Lecturer
{
    public class LecturerFacade : ILecturerFacade
    {
        private readonly Repository<Entities.Lecturer> _lecturerRepository;
        private readonly Repository<Entities.RequestPartimInformation> _requestPartimInformationRepository; 

        public LecturerFacade(IUnitOfWork unitOfWork)
        {
            _lecturerRepository = unitOfWork.Repository<Entities.Lecturer>();
            _requestPartimInformationRepository = unitOfWork.Repository<Entities.RequestPartimInformation>();

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
            //return GetRequestEntities(email, status).ProjectTo<RequestPartimInformation>();
            return _lecturerRepository.Table.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.RequestPartimInformations)
                .Where(a => a.Status == (Entities.Status)(int)status)
                .ProjectTo<RequestPartimInformation>();
        }

        public IQueryable<RequestPartimInformation> GetUntreadedRequestsDistinct(string email) //students
        {
           // var requestPartimInformations = GetRequestEntities(email, Status.Untreated);

            var result = from requestPartimInformation in _lecturerRepository.Table.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.RequestPartimInformations)
                .Where(a => a.Status == Entities.Status.Untreated)
                //.ProjectTo<RequestPartimInformation>()
                         where requestPartimInformation.Id > 0
                         //group requestPartimInformation by requestPartimInformation.Request.File.Student.Id
                         group requestPartimInformation by requestPartimInformation.Request.File.Student.Id
                            into groups
                            select groups.FirstOrDefault();

            return result.ProjectTo<RequestPartimInformation>();
        }

        public IQueryable<RequestPartimInformation> GetUntreadedRequestPartims(string email) //partims
        {
            var result = from requestPartimInformation in _lecturerRepository.Table.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.RequestPartimInformations)
                .Where(a => a.Status == Entities.Status.Untreated)
                         where requestPartimInformation.Id > 0
                         group requestPartimInformation by requestPartimInformation.PartimInformation
                            into groups
                         select groups.FirstOrDefault();

            return result.ProjectTo<RequestPartimInformation>();
        }

        public bool Approve(int requestPartimInformationId, bool isApproved, string email)
        {
            var requestPartimInformation = _requestPartimInformationRepository.GetById(requestPartimInformationId);

            if (requestPartimInformation.Status != (Entities.Status)(int)Status.Untreated || requestPartimInformation.PartimInformation.Lecturer.Email != email)
                return false;

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
    }
}