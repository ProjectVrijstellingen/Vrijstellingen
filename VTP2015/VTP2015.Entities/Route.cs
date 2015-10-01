using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Route : BaseEntity
    {
        public string Name { get; set; }
        public int EducationId { get; set; }

        public virtual Education Education { get; set; }
        public virtual ICollection<PartimInformation> PartimInformation { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
