using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Module
    {
        public string ModuleId { get; set; }
        public string Naam { get; set; }

        public ICollection<PartimInformatie> PartimInformatie { get; set; }
    }
}
