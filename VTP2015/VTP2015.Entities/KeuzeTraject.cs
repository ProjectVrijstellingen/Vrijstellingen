using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTP2015.Entities
{
    public class KeuzeTraject
    {
        public string Name;

        public virtual Opleiding Opleiding { get; set; }
        
        public virtual ICollection<PartimInformatie> PartimInformatie { get; set; }
        public virtual ICollection<Student> Studenten { get; set; }
    }
}
