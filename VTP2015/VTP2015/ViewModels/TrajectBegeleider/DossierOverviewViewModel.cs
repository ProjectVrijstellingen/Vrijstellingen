namespace VTP2015.ViewModels.TrajectBegeleider
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