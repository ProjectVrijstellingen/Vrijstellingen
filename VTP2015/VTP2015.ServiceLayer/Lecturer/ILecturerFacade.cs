using System.Linq;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.ServiceLayer.Lecturer
{
    public interface ILecturerFacade
    {
        IQueryable<Request> GetUntreadedRequests(string email);
        IQueryable<Request> GetUntreadedRequestsDistinct(string email);
        bool Approve(int requestId, bool isApproved, string email);
    }
}
