using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IDossierRepository
    {
        Dossier GetById(int dossierId);
        IQueryable<Dossier> GetAll();
        IQueryable<Dossier> GetAllNonEmpty();
        IQueryable<Dossier> GetByStudent(string email);
        Dossier Insert(Dossier entity);
        bool IsDossierFromStudent(string email, int dossierId);
        IQueryable<Dossier> GetFromBegeleider(string email, string academiejaar);
    }
}