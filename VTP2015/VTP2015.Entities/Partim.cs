using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Partim : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<PartimInformation> PartimInformation { get; set; }
    }
}
