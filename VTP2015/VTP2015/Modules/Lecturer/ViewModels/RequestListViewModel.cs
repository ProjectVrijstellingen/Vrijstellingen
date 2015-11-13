using System.Collections.Generic;

namespace VTP2015.Modules.Lecturer.ViewModels
{
    public class RequestListViewModel
    {
        public int RequestId { get; set; }
        public ServiceLayer.Lecturer.Models.Student Student { get; set; }
        public string ModuleName { get; set; }
        public string PartimName { get; set; }
        public string Argumentation { get; set; }
        public IEnumerable<EvidenceViewModel> Evidence { get; set; }
    }
}