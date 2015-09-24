using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Opleiding
    {
        public int OpleidingId { get; set; }
        public string Naam { get; set; }

        public virtual TrajectBegeleider TrajectBegeleider { get; set; }
        public virtual ICollection<Student> Studenten { get; set; }
    }
}
