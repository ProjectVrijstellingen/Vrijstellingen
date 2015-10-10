using System.Collections.Generic;
using VTP2015.Entities;
using VTP2015.Modules.Lecturer.ViewModels;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class AanvraagDetailsViewModel
    {
        public int AanvraagId { get; set; }
        public string DossierId { get; set; }
        public Status Status { get; set; }
        public string Argumentatie { get; set; }
        public PartimInformation PartimInformation { get; set; }
        public IEnumerable<BewijsViewModel> Bewijzen { get; set; }
    }
}