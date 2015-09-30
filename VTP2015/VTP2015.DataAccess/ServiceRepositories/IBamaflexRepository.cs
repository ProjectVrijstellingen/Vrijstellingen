using System.Collections.Generic;
using VTP2015.DataAccess.Bamaflex;

namespace VTP2015.DataAccess.ServiceRepositories
{
    public interface IBamaflexRepository
    {
        Opleiding GetEducationByStudentCode(string code);
    }
}