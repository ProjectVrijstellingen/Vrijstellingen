using VTP2015.Entities;
using User = VTP2015.DataAccess.Identity.User;

namespace VTP2015.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Student GetByEmail(string email);
        void SyncStudentByUser(User user);
        string GetStudentIdByEmail(string email);
        bool IsAanvraagFromStudent(int dossierId, string supercode, string email);
    }
}
