using System;
using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class File : BaseEntity
    {
        public int StudentId { get; set; }
        public string AcademicYear { get; set; }
        public DateTime DateCreated { get; set; }
        public FileStatus FileStatus { get; set; }
        public int EducationId { get; set; }    

        public virtual Education Education { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Request> Requests { get; set; }

    }
}