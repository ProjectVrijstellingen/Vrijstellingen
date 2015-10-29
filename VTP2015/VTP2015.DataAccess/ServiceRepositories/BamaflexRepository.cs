using System.Collections.Generic;
using VTP2015.DataAccess.Bamaflex;
using VTP2015.Entities;

namespace VTP2015.DataAccess.ServiceRepositories
{
    public class BamaflexRepository : IBamaflexRepository
    {
        private readonly Facade _bamaflexService = new Facade();

        public Opleiding GetEducationByStudentCode(string code)
        {
            return _bamaflexService
                .GetStudent("b66352b3-938e-4e8e-981b-2008a8c5171d")
                .Departementen[0]
                .Opleidingen[0];
        }

        public ICollection<OpleidingsProgramma> GetRoutes(Education education)
        {
            return _bamaflexService.GetOpleidingsprogrammaByOpleidingscode(education.Code).KeuzeTrajecten;
        }

        public PartimInformatie GetPartimInformationBySupercode(string supercode)
        {
            return _bamaflexService.GetPartimInformatie(supercode)[0];
        }

        public ICollection<Opleiding> GetEducations()
        {
            return _bamaflexService.GetOpleidingen();
        }
    }
}