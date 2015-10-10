using System.Collections.Generic;

namespace VTP2015.Modules.Lecturer.ViewModels
{
    public class AanvraagListViewModel
    {
        public int AanvraagId { get; set; }
        public string StudentId { get; set; }
        public string ModuleName { get; set; }
        public string PartimName { get; set; }
        public string Argumentatie { get; set; }
        public IEnumerable<BewijsViewModel> Bewijzen { get; set; }
    }
}