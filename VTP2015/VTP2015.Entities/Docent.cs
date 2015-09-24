using System;
using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Docent
    {
        public int DocentId { get; set; }
        public string Email { get; set; }
        public DateTime InfoMail { get; set; }
        public DateTime WarningMail { get; set; }

        public virtual ICollection<PartimInformatie> PartimInformatie { get; set; }
    }
}
