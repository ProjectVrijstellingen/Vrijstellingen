using System;
using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Request : BaseEntity
    {
        public Request()
        {
            LastChanged = DateTime.Now;
        }

        public string Name { get; set; }
        public string Argumentation { get; set; }
        public DateTime LastChanged { get; set; }
        public int FileId { get; set; }
       

        public virtual ICollection<RequestPartimInformation> RequestPartimInformations { get; set; }
        public virtual File File { get; set; }
        public virtual ICollection<Evidence> Evidence { get; set; }
        public virtual ICollection<PrevEducation> PrevEducations { get; set; } 
    }
}