using System.Collections.Generic;

namespace VTP2015.Modules.Counselor.DTOs
{
    public class Partim
    {
        public string Name { get; set; }
        public int RequestId { get; set; }
        public int FileId { get; set; }
        public string Status { get; set; }
        public int PartimInformationId { get; set; }
        public IEnumerable<PrevEducation> PrevEducations { get; set; }
        public IEnumerable<Evidence> Evidence { get; set; }
    }
}