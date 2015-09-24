using System.Collections.Generic;

namespace VTP2015.ViewModels.Student
{
    public class AanvraagDetailViewModel
    {
        public string SuperCode { get; set; }
        public string ModuleNaam { get; set; }
        public string PartimNaam { get; set; }
        public string Argumentatie { get; set; }
        public IEnumerable<BewijsListViewModel> Bewijzen { get; set; }
    }
}