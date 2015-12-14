using System;

namespace VTP2015.ServiceLayer.Lecturer.Models
{
    public class File
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; }
        public string Education { get; set; }
        public string AcademicYear { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Editable { get; set; }
    }
}
