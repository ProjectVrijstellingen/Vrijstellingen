﻿using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Student : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? EducationId { get; set; }

        public virtual Education Education { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<PartimInformation> PartimInformation { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Evidence> Evidence { get; set; }
    }
}