using VTP2015.Modules.Shared;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class DossierOverviewViewModel : IViewModel
    {
        public string DossierId { get; set; }
        public string StudentVoornaam { get; set; }
        public string StudentNaam { get; set; }
        public int PercentageVoltooid { get; set; }
        public string AfstudeerRichting { get; set; }
        public string AcademieJaar { get; set; }
    }
}