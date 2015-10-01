using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Evidence : BaseEntity
    {
        public int StudentId { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public virtual Student Student { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}