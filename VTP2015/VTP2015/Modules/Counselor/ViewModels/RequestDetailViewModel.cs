using System.Collections.Generic;
using VTP2015.Entities;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class RequestDetailViewModel
    {
        public int RequestId { get; set; }
        public string FileId { get; set; }
        public Status Status { get; set; }
        public string Argumentation { get; set; }
        public PartimInformationViewModel PartimInformation { get; set; }
        public IEnumerable<EvidenceViewModel> Evidence { get; set; }
    }
}