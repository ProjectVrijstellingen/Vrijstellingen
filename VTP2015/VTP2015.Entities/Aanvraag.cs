using System;
using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Aanvraag
    {
        public Aanvraag()
        {
            LastChanged = DateTime.Now;
            Status = 0;
        }

        public int AanvraagId { get; set; }
        public int DossierId { get; set; }
        public string SuperCode { get; set; }
        public string Argumentatie { get; set; }
        public DateTime LastChanged { get; set; }
        public Status Status { get; set; }

        public virtual PartimInformatie PartimInformatie { get; set; }
        public virtual Dossier Dossier { get; set; }
        public virtual ICollection<Bewijs> Bewijzen { get; set; }
    }
}