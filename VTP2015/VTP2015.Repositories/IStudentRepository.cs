using System.Linq;
using VTP2015.DataAccess.Identity;
using VTP2015.Entities;

namespace VTP2015.Repositories
{
    public interface IStudentRepository
    {
        void SyncStudentByUser(User user);
        string GetStudentIdByEmail(string email);
        bool IsAanvraagFromStudent(int dossierId, string supercode, string email);
    }
}
