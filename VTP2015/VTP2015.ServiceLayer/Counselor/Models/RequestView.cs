using System.Collections.Generic;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class RequestView
    {
        public string Module { get; set; }
        public IEnumerable<PartimView> Partims { get; set; } 
        public IEnumerable<int> PrevEducationId { get; set; } 
        public IEnumerable<int> EvidenceIds { get; set; }
    }
}