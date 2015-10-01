using System;
using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Request : BaseEntity
    {
        public Request()
        {
            LastChanged = DateTime.Now;
            Status = 0;
        }

        public string Argumentation { get; set; }
        public DateTime LastChanged { get; set; }
        public Status Status { get; set; }
        public int PartimInformationId { get; set; }
        public int FileId { get; set; }
       

        public virtual PartimInformation PartimInformation { get; set; }
        public virtual File File { get; set; }
        public virtual ICollection<Evidence> Evidence { get; set; }
    }
}