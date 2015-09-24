using System.Collections.Generic;
using VTP2015.DataAccess.Bamaflex;

namespace VTP2015.Repositories.Remote_Services
{
    public interface IBamaflexRepository
    {


        string GetPartimNameBySuperCode(string supercode);
        string GetModuduleNameBySuperCode(string supercode);
        IEnumerable<PartimInformatie> GetPartimInformatieList(string studentId, string academieJaar);
        string GetOpleidingByStudentId(string id);
        string GetAfstudeerRichtingByStudentId(string id, string academieJaar);
        bool IsSuperCodeFromStudent(string superCode, string studentId, string academieJaar);
        IEnumerable<Opleiding> GetOpleidingen();
        string GetDocentFromPartim(string superCode);
    }
}
