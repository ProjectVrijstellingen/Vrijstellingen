using System.Linq;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.ServiceLayer.Lecturer
{
    public interface ILecturerFacade
    {
        IQueryable<RequestPartimInformation> GetRequests(string email, Status status);
        IQueryable<Models.Student> GetUntreadedStudent(string email);
        IQueryable<PartimInformation> GetPartims(string email);
        bool Approve(int requestId, bool isApproved, string email, int motivation);
        bool hasAny(string email, Status status);
        IQueryable<Motivation> GetMotivations();
        bool RemovePartimLecturer(string supercode);
    }
}
