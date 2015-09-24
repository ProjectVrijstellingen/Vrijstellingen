using System.Collections.Generic;
using VTP2015.Entities;

namespace VTP2015.ViewModels.TrajectBegeleider
{
    public class OpleidingSelectViewModel
    {
        public string SelectedOpleiding { get; set; }
        public List<OpleidingViewModel> Opleidingen { get; set; }
    }
}