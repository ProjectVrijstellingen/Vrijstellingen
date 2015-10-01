﻿using System;

namespace VTP2015.ServiceLayer.Student.Models
{
    public class File
    {
        public string StudentMail { get; set; }
        public string Specialization { get; set; }
        public string AcademicYear { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Editable { get; set; }

    }
}
