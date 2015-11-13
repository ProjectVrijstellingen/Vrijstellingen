using System.Collections.Generic;
using System.Linq;

namespace VTP2015.Modules.Student.ViewModels
{
    public class RequestDetailViewModel
    {
        public int FileId { get; set; }
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string PartimName { get; set; }
        public string Code { get; set; }
        public string Argumentation { get; set; }
        public bool Submitted { get; set; }
        public IEnumerable<EvidenceListViewModel> Evidence { get; set; }
    }
}