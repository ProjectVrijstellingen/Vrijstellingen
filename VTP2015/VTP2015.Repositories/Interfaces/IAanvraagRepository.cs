using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IAanvraagRepository
    {
        IQueryable<Aanvraag> GetAll();
        IQueryable<Aanvraag> GetByDossierId(int dossierId);
        IQueryable<Aanvraag> GetOnbehandeldeAanvragen(string email);
        IQueryable<Aanvraag> GetOnbehandeldeAanvragenDistinct(string email);
        bool Beoordeel(int aanvraagId, bool isGoedgekeurd, string email);
        bool SyncAanvraagInDossier(Aanvraag aanvraag);
        bool Delete(int dossierId, string supercode);
        string GetEmailByAanvraagId(int aanvraagId);
        Aanvraag GetAanvraagById(int aanvraagId);
    }
}
