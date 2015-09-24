using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IAanvraagRepository
    {
        IQueryable<Request> GetAll();
        IQueryable<Request> GetByDossierId(int dossierId);
        IQueryable<Request> GetOnbehandeldeAanvragen(string email);
        IQueryable<Request> GetOnbehandeldeAanvragenDistinct(string email);
        bool Beoordeel(int aanvraagId, bool isGoedgekeurd, string email);
        bool SyncAanvraagInDossier(Request request);
        bool Delete(int dossierId, string supercode);
        string GetEmailByAanvraagId(int aanvraagId);
        Request GetAanvraagById(int aanvraagId);
    }
}
