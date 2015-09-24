using System.Collections.Generic;
using VTP2015.Entities;
using VTP2015.ViewModels.Docent;

namespace VTP2015.ViewModels.TrajectBegeleider
{
    public class AanvraagDetailsViewModel
    {
        public int AanvraagId { get; set; }
        public string DossierId { get; set; }
        public Status Status { get; set; }
        public string Argumentatie { get; set; }
        public PartimInformatie PartimInformatie { get; set; }
        public IEnumerable<BewijsViewModel> Bewijzen { get; set; }
    }
}