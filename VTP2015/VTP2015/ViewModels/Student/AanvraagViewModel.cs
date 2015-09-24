using System.Collections.Generic;

namespace VTP2015.ViewModels.Student
{
    public class AanvraagViewModel
    {
        public int DossierId { get; set; }
        public string SuperCode { get; set; }
        public string Argumentatie { get; set; }
        public IEnumerable<int> Bewijzen { get; set; }
    }
}   