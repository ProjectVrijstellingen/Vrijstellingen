using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.Bamaflex;

namespace VTP2015.DataAccess.ServiceRepositories
{
    public class BamaflexRepository : IBamaflexRepository
    {
        private readonly Facade _bamaflexService = new Facade();

        public Opleiding GetEducationByStudentCode(string code)
        {
            return _bamaflexService.GetStudent((code.Split('|')[0])).Departementen[0].Opleidingen[0];
        }
    }
}