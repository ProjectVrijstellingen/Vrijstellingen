using System.Collections.Generic;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class EducationSelectViewModel
    {
        public string SelectedOpleiding { get; set; }
        public IEnumerable<EducationViewModel> Opleidingen { get; set; }
    }
}