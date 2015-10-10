using System.Collections.Generic;

namespace VTP2015.Modules.Student.ViewModels
{
    public class RequestDetailViewModel
    {
        public string SuperCode { get; set; }
        public string ModuleName { get; set; }
        public string PartimName { get; set; }
        public string Argumentation { get; set; }
        public IEnumerable<EvidenceListViewModel> Evidence { get; set; }
    }
}