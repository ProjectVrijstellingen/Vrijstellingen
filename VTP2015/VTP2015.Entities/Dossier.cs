using System;
using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Dossier
    {
        public int DossierId { get; set; }
        public string StudentId { get; set; }
        public string AcademieJaar { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public bool Editable { get; set; }

        public virtual Opleiding Opleiding { get; set; }
        public virtual KeuzeTraject KeuzeTraject { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Aanvraag> Aanvragen { get; set; }

    }
}