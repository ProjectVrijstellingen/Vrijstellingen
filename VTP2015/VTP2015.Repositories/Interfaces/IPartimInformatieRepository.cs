using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IPartimInformatieRepository
    {
        IQueryable<PartimInformation> GetAangevraagdePartims(string email, int dossierId);
        IQueryable<PartimInformation> GetBeschikbarePartims(string email, int dossierId);
        PartimInformation GetBySuperCode(string superCode);
        bool SyncStudentPartims(string email, string academieJaar);

    }
}
