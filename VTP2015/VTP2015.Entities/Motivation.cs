using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Motivation:BaseEntity
    {
        public string Text { get; set; }

        public virtual ICollection<RequestPartimInformation> RequestPartimInformations { get; set; } 
    }
}
