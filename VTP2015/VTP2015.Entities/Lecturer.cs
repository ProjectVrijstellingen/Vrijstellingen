using System;
using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Lecturer : BaseEntity
    {
        public string Email { get; set; }
        public DateTime InfoMail { get; set; }
        public DateTime WarningMail { get; set; }

        public virtual ICollection<PartimInformation> PartimInformation { get; set; }
    }
}
