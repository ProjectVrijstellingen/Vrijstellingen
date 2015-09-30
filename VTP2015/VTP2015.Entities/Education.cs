﻿using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Education : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual Counselor Counselor { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
