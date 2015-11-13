using System.Linq;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.ServiceLayer.Lecturer
{
    public interface ILecturerFacade
    {
        IQueryable<RequestPartimInformation> GetRequests(string email, Status status);
        IQueryable<RequestPartimInformation> GetUntreadedRequestsDistinct(string email);
        bool Approve(int requestId, bool isApproved, string email);
        bool hasAny(string email, Status status);
    }
}
