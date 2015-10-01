using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Route
    {
        public string Name;
        public virtual Education Education { get; set; }
        
        public virtual ICollection<PartimInformation> PartimInformatie { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
