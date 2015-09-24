using System;

namespace VTP2015.ViewModels.Student
{
    public class DossierListViewModel
    {
        public int DossierId { get; set; }

        public string Omschrijving
        {
            get { return Opleiding + AfstudeerRichting + AanmaakDatum; }
        }

        public bool Editable { get; set; }
        public string Opleiding { get; set; }
        public string AfstudeerRichting { get; set; }
        public DateTime AanmaakDatum { get; set; }
    }
}