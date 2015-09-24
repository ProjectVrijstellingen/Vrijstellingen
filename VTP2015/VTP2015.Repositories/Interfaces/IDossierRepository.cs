using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IDossierRepository
    {
        File GetById(int dossierId);
        IQueryable<File> GetAll();
        IQueryable<File> GetAllNonEmpty();
        IQueryable<File> GetByStudent(string email);
        File Insert(File entity);
        bool IsDossierFromStudent(string email, int dossierId);
        IQueryable<File> GetFromBegeleider(string email, string academiejaar);
    }
}