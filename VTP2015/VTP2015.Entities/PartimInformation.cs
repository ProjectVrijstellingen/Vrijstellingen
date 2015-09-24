using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class PartimInformation : BaseEntity
    {
        public string SuperCode { get; set; }
        public string PartimId { get; set; }
        public string ModuleId { get; set; }
        public int LecturerId { get; set; }

        public virtual Partim Partim { get; set; }
        public virtual Module Module { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

    }
}
