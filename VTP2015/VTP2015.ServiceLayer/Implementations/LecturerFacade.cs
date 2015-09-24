using System.Linq;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Interfaces;

namespace VTP2015.ServiceLayer.Implementations
{
    public class LecturerFacade : ILecturerFacade
    {
        private readonly Repository<Request> _requestRepository;
        private readonly Repository<Lecturer> _lecturerRepository;

        public LecturerFacade(Repository<Request> requestRepository, Repository<Lecturer> lecturerRepository)
        {
            _requestRepository = requestRepository;
            _lecturerRepository = lecturerRepository;
        }

        public IQueryable<Request> GetUntreadedRequests(string email)
        {
            return _lecturerRepository.Table.Where(b => b.Email == email)
                .SelectMany(p => p.PartimInformation)
                .SelectMany(p => p.Requests)
                .Where(a => a.Status == Status.Untreated);

        }

        public IQueryable<Request> GetUntreadedRequestsDistinct(string email)
        {
            var requests = GetUntreadedRequests(email);

            var result = from request in requests
                         where request.Id > 0
                         group request by request.File.Student.Id
                            into groups
                            select groups.FirstOrDefault();

            return result;
        }

        public bool Approve(int requestId, bool isApproved, string email)
        {
            var request = _requestRepository.GetById(requestId);

            if (request.Status != Status.Untreated || request.PartimInformation.Lecturer.Email != email)
                return false;

            request.Status = isApproved ? Status.Approved : Status.Rejected;
            _requestRepository.Update(request);

            return true;
        }
    }
}