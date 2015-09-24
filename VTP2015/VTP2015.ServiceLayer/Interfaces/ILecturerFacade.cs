using System.Linq;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Interfaces
{
    public interface ILecturerFacade
    {
        IQueryable<Request> GetUntreadedRequests(string email);
        IQueryable<Request> GetUntreadedRequestsDistinct(string email);
        bool Approve(int requestId, bool isApproved, string email);
    }
}
