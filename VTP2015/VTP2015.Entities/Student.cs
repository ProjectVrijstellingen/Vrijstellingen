using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Student : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int EducationId { get; set; }
        public int? RouteId { get; set; }

        public virtual Education Education { get; set; }
        public virtual Route Route { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Evidence> Evidence { get; set; }
        public virtual ICollection<PrevEducation> PrevEducations { get; set; } 
    }
}