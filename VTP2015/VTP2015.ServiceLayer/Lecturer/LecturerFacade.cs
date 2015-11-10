using System.Linq;
using AutoMapper.QueryableExtensions;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Lecturer.Mappings;

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

        private IQueryable<Entities.RequestPartimInformation> GetUntreadedRequestEntities(string email)
        {
            return _lecturerRepository.Table.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.RequestPartimInformations)
                .Where(a => a.Status == Status.Untreated);
        }

        public IQueryable<Models.RequestPartimInformation> GetUntreadedRequests(string email)
        {
            return GetUntreadedRequestEntities(email).ProjectTo<Models.RequestPartimInformation>();
        }

        public IQueryable<Models.RequestPartimInformation> GetUntreadedRequestsDistinct(string email)
        {
            var requestPartimInformations = GetUntreadedRequestEntities(email);

            var result = from requestPartimInformation in requestPartimInformations
                         where requestPartimInformation.Id > 0
                         group requestPartimInformation by requestPartimInformation.Request.File.Student.Id
                            into groups
                            select groups.FirstOrDefault();

            return result.ProjectTo<Models.RequestPartimInformation>();
        }

        public bool Approve(int requestPartimInformationId, bool isApproved, string email)
        {
            var requestPartimInformation = _requestPartimInformationRepository.GetById(requestPartimInformationId);

            if (requestPartimInformation.Status != Status.Untreated || requestPartimInformation.PartimInformation.Lecturer.Email != email)
                return false;

            requestPartimInformation.Status = isApproved ? Status.Approved : Status.Rejected;
            _requestPartimInformationRepository.Update(requestPartimInformation);
            
            return true;
        }
    }
}