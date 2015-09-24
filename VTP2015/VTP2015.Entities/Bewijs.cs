using System.Collections.Generic;

namespace VTP2015.Entities
{
    public class Bewijs
    {
        public int BewijsId { get; set; }
        public string StudentId { get; set; }
        public string Omschrijving { get; set; }
        public string Path { get; set; }

        public virtual Student Student { get; set; }
        public virtual ICollection<Aanvraag> Aanvragen { get; set; }
    }
}