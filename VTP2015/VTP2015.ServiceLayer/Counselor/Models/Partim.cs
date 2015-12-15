using System.Collections.Generic;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class Partim
    {
        public string Name { get; set; }
        public int FileId { get; set; }
        public int RequestId { get; set; }
        public int PartimInformationId { get; set; }
        public IEnumerable<PrevEducation> PrevEducations { get; set; } 
        public IEnumerable<Evidence> Evidence { get; set; }
        public Status Status { get; set; }
    }
}
