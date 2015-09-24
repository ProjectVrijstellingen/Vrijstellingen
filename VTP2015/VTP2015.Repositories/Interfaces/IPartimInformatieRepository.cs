using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IPartimInformatieRepository
    {
        IQueryable<PartimInformatie> GetAangevraagdePartims(string email, int dossierId);
        IQueryable<PartimInformatie> GetBeschikbarePartims(string email, int dossierId);
        PartimInformatie GetBySuperCode(string superCode);
        bool SyncStudentPartims(string email, string academieJaar);

    }
}
