﻿using System;
using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class File : BaseEntity
    {
        public string StudentId { get; set; }
        public string Specialization { get; set; }
        public string AcademicYear { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Editable { get; set; }

        public virtual Opleiding Opleiding { get; set; }
        public virtual KeuzeTraject KeuzeTraject { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

    }
}