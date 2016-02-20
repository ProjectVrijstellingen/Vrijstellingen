using System.Collections.Generic;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.Modules.Lecturer.ViewModels
{
    public class RequestListViewModel
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string PartimName { get; set; }
        public string Argumentation { get; set; }
        public IEnumerable<Evidence> Evidence { get; set; }
        public ServiceLayer.Lecturer.Models.Student Student { get; set; }
        public string SuperCode { get; set; }
        public Motivation Motivation { get; set; }
        public int Semester { get; set; }
        public IEnumerable<PrevEducation> PrevEducation { get; set; }
    }
}