using System.Collections.Generic;
using VTP2015.DataAccess.Bamaflex;
using VTP2015.Entities;

namespace VTP2015.DataAccess.ServiceRepositories
{
    class MockIbamaflexRepository : IBamaflexRepository
    {
        public Opleiding GetEducationByStudentCode(string code)
        {
            return new Opleiding
            {
                Code = "",
                Naam = ""
            };
        }

        public ICollection<OpleidingsProgramma> GetRoutes(Education education)
        {
            return new List<OpleidingsProgramma>
            {
                new OpleidingsProgramma
                {
                    Naam = "",
                    Modules = new []
                    {
                        new OpleidingsProgrammaOnderdeel
                        {
                            Supercode = ""
                        }, 
                    }
                }
            };
        }

        public PartimInformatie GetPartimInformationBySupercode(string supercode)
        {
            return new PartimInformatie
            {
                Supercode = new Supercode {  Supercode1 = "" },
                Partim = new Item { Id = "", Naam = "" },
                Module = new Item { Id = "", Naam = ""}
            };
        }

        public ICollection<Opleiding> GetEducations()
        {
            return new List<Opleiding>
            {
                new Opleiding { Code = "", Naam = "" }
            };
            throw new System.NotImplementedException();
        }
    }
}
