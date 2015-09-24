using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Partim
    {
        public string PartimId { get; set; }
        public string Naam { get; set; }

        public ICollection<PartimInformatie> PartimInformatie { get; set; }
    }
}
