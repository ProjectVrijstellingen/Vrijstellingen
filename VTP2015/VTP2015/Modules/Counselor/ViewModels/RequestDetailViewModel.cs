using System.Collections.Generic;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class RequestDetailViewModel
    {
        public int RequestId { get; set; }
        public string FileId { get; set; }
        public StatusViewModel StatusViewModel { get; set; }
        public string Argumentation { get; set; }
        public string PartimName { get; set; }
        public string ModuleName { get; set; }
        public IEnumerable<EvidenceViewModel> Evidence { get; set; }
    }
}