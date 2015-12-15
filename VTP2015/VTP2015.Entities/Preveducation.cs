using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class PrevEducation : BaseEntity
    {
        public string Education { get; set; }
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}