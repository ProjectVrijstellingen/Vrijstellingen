using System.Collections.Generic;
using VTP2015.DataAccess.Bamaflex;
using VTP2015.Entities;

namespace VTP2015.DataAccess.ServiceRepositories
{
    public interface IBamaflexRepository
    {
        Opleiding GetEducationByStudentCode(string code);
        OpleidingsProgramma GetEducation(Education education);
        PartimInformatie GetPartimInformationBySupercode(string supercode);
        ICollection<Opleiding> GetEducations();
    }
}