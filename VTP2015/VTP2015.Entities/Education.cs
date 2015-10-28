using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Education : BaseEntity
    {
        public string Name { get; set; }
        public string AcademicYear { get; set; } 
        public string Code { get; set; }


        public virtual ICollection<File> Files { get; set; } 
        public virtual ICollection<Counselor> Counselors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
