
using System;
using System.Collections.Generic;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class FileView
    {
        public IEnumerable<Motivation> MotivationList { get; set; }  
        public string Education { get; set; }
        public string Counselor { get; set; }
        public DateTime DateCreated { get; set; }
        public string AcademicYear { get; set; }
        public StudentView Student { get; set; }
        public IEnumerable<RequestView> Requests { get; set; }
        public IEnumerable<EvidenceView> Evidence { get; set; }
        public IEnumerable<PrevEducationView> PrevEducations { get; set; } 
    }
}
