using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTP2015.Entities
{
    public class Route
    {
        public string Name;
        public virtual Education Edcucation { get; set; }
        
        public virtual ICollection<PartimInformation> PartimInformatie { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
