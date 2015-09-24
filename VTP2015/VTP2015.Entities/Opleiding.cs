using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Opleiding
    {
        public string code { get; set; }
        public string Name { get; set; }
        public string AcademicYear { get; set; } 

        public virtual TrajectBegeleider TrajectBegeleider { get; set; }

        public virtual ICollection<Student> Studenten { get; set; }
        public virtual ICollection<KeuzeTraject> KeuzeTrajecten { get; set; }
    }
}
