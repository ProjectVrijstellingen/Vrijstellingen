using System.Collections.Generic;

namespace VTP2015.Modules.Student.ViewModels
{
    public class RequestDetailViewModel
    {
        public int RequestId { get; set; }
        public string Naam { get; set; }
        public string Argumentation { get; set; }
        public IEnumerable<EvidenceListViewModel> Evidence { get; set; }
        public IEnumerable<RequestPartimInformationViewModel> RequestPartimInformation { get; set; } 
    }
}