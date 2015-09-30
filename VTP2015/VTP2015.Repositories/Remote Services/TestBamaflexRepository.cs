using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.Bamaflex;

namespace VTP2015.Repositories.Remote_Services
{
    public class TestBamaflexRepository : IBamaflexRepository
    {
        public string GetModuduleNameBySuperCode(string supercode)
        {
            return "Module 1";
        }

        public PartimInformatie GetTestPartim()
        {
            throw new System.NotImplementedException();
        }

        public string GetPartimNameBySuperCode(string supercode)
        {
            return "Partim 1";
        }

        public IEnumerable<PartimInformatie> GetPartimInformatieList(string studentId, string academieJaar)
        {
            IEnumerable<PartimInformatie> informatie = new List<PartimInformatie>
            {
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008809006758"},
                    Partim = new Item {Id = "1", Naam = "Actuele Webtechnologie"},
                    Module = new Item {Id = "8809", Naam = "Webontiwkkeling III"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008809005027"},
                    Partim = new Item {Id = "2", Naam = "CMS"},
                    Module = new Item {Id = "8809", Naam = "Webontiwkkeling III"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008809008846"},
                    Partim = new Item {Id = "3", Naam = "Xml en webservices"},
                    Module = new Item {Id = "8809", Naam = "Webontiwkkeling III"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008810008849"},
                    Partim = new Item {Id = "4", Naam = "Java Servlets en JSP"},
                    Module = new Item {Id = "8810", Naam = "Webontiwkkeling IV"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008810001988"},
                    Partim = new Item {Id = "5", Naam = "ASP.NET"},
                    Module = new Item {Id = "8810", Naam = "Webontiwkkeling IV"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008810010594"},
                    Partim = new Item {Id = "6", Naam = "Mobile Web apps"},
                    Module = new Item {Id = "8810", Naam = "Webontiwkkeling IV"}
                }
            };
            return informatie.ToArray();

        }

        public IEnumerable<PartimInformatie> GetPartimInformatieList(string email, int dossierId)
        {
            IEnumerable<PartimInformatie> informatie = new List<PartimInformatie>
            {
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008809006758"},
                    Partim = new Item {Naam = "Actuele Webtechnologie"},
                    Module = new Item {Id = "8809", Naam = "Webontiwkkeling III"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008809005027"},
                    Partim = new Item {Naam = "CMS"},
                    Module = new Item {Id = "8809", Naam = "Webontiwkkeling III"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008809008846"},
                    Partim = new Item {Naam = "Xml en webservices"},
                    Module = new Item {Id = "8809", Naam = "Webontiwkkeling III"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008810008849"},
                    Partim = new Item {Naam = "Java Servlets en JSP"},
                    Module = new Item {Id = "8810", Naam = "Webontiwkkeling IV"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008810001988"},
                    Partim = new Item {Naam = "ASP.NET"},
                    Module = new Item {Id = "8810", Naam = "Webontiwkkeling IV"}
                },
                new PartimInformatie
                {
                    Supercode = new Supercode {Supercode1 = "06000840008810010594"},
                    Partim = new Item {Naam = "Mobile Web apps"},
                    Module = new Item {Id = "8810", Naam = "Webontiwkkeling IV"}
                }
            };
            return informatie.ToArray();

        }

        public string GetOpleidingByStudentId(string id)
        {
            return "Toegepaste Informatica";
        }

        public string GetAfstudeerRichtingByStudentId(string id, string academiejaar)
        {
            return "SSD";
        }

        public bool IsSuperCodeFromStudent(string superCode, string email, string academieJaar)
        {
            return true;
        }

        public IEnumerable<Opleiding> GetOpleidingen()
        {
            IEnumerable<Opleiding> opleidingen = new List<Opleiding>
            {
                new Opleiding
                {
                    Id = "1",
                    Naam = "Toegepaste Informatica"
                },
                new Opleiding
                {
                    Id = "2",
                    Naam = "Bedrijfsmanagement"
                }

            };
            return opleidingen;
        }


        public string GetDocentFromPartim(string superCode)
        {
            return "docent@howest.be";
        }

        public IEnumerable<DataAccess.Bamaflex.PartimInformatie> GetPartimInformatieList(Entities.Opleiding opleiding)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Entities.Opleiding> IBamaflexRepository.GetOpleidingen()
        {
            throw new NotImplementedException();
        }

        public ICollection<Entities.KeuzeTraject> GetKeuzeTrajecten(Entities.Opleiding opleiding)
        {
            throw new NotImplementedException();
        }

        ICollection<OpleidingsProgramma> IBamaflexRepository.GetKeuzeTrajecten(Entities.Opleiding opleiding)
        {
            throw new NotImplementedException();
        }

        public DataAccess.Bamaflex.PartimInformatie GetPartimInformationBySupercode(string supercode)
        {
            throw new NotImplementedException();
        }
    }
}
