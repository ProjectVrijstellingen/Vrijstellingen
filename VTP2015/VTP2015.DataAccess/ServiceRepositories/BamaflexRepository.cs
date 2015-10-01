using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.Bamaflex;
using VTP2015.Entities;

namespace VTP2015.DataAccess.ServiceRepositories
{
    public class BamaflexRepository : IBamaflexRepository
    {
        private readonly Facade _bamaflexService = new Facade();

        public Opleiding GetEducationByStudentCode(string code)
        {
            return _bamaflexService.GetStudent((code.Split('|')[0])).Departementen[0].Opleidingen[0];
        }

        public ICollection<OpleidingsProgramma> GetRoutes(Education education)
        {
            return _bamaflexService.GetOpleidingsprogrammaByOpleidingscode(education.Code).KeuzeTrajecten;
        }

        public PartimInformatie GetPartimInformationBySupercode(string supercode)
        {
            return _bamaflexService.GetPartimInformatie(supercode)[0];
        }
    }
}