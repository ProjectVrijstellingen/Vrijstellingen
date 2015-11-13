using System;

namespace VTP2015.ServiceLayer.Student.Models
{
    public class File
    {
        public int Id { get; set; }
        public string StudentMail { get; set; }
        public string Education { get; set; }
        public string AcademicYear { get; set; }
        public DateTime DateCreated { get; set; }
        public FileStatus FileStatus { get; set; }
    }
}
