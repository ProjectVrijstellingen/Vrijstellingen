using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class PartimInformatie
    {
        public string SuperCode { get; set; }
        public string PartimId { get; set; }
        public string ModuleId { get; set; }
        public int DocentId { get; set; }

        public virtual Partim Partim { get; set; }
        public virtual Module Module { get; set; }
        public virtual Docent Docent { get; set; }
        public virtual KeuzeTraject KeuzeTraject { get; set; }
        public virtual ICollection<Aanvraag> Aanvragen { get; set; }

    }
}
