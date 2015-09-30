using System.Collections.Generic;
using VTP2015.DataAccess.Bamaflex;

namespace VTP2015.Repositories.Remote_Services
{
    public interface IBamaflexRepository
    {


        string GetPartimNameBySuperCode(string supercode);
        string GetModuduleNameBySuperCode(string supercode);
        string GetOpleidingByStudentId(string id);
        string GetAfstudeerRichtingByStudentId(string id, string academieJaar);
        bool IsSuperCodeFromStudent(string superCode, string studentId, string academieJaar);
        IEnumerable<Entities.Opleiding> GetOpleidingen();
        string GetDocentFromPartim(string superCode);
        ICollection<OpleidingsProgramma> GetKeuzeTrajecten(Entities.Opleiding opleiding);
        PartimInformatie GetPartimInformationBySupercode(string supercode);
    }
}
