using System.Linq;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.ServiceLayer.Lecturer
{
    public interface ILecturerFacade
    {
        IQueryable<RequestPartimInformation> GetUntreadedRequests(string email);
        IQueryable<RequestPartimInformation> GetUntreadedRequestsDistinct(string email);
        bool Approve(int requestId, bool isApproved, string email);
    }
}
