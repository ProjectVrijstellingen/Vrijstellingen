using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Student
    {
        public string StudentId { get; set; }
        public string Naam { get; set; }
        public string VoorNaam { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? OpleidingId { get; set; }

        public virtual Opleiding Opleiding { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<PartimInformatie> PartimInformatie { get; set; }
        public virtual ICollection<Dossier> Dossiers { get; set; }
        public virtual ICollection<Bewijs> Bewijzen { get; set; }
    }
}