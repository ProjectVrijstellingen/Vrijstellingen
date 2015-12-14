using System.Collections.Generic;

namespace VTP2015.Modules.Counselor.ViewModels.Models
{
    public class Partim
    {
        public string Name { get; set; }
        public int FileId { get; set; }
        public int RequestId { get; set; }
        public string Argumentation { get; set; }
        public int PartimInformationId { get; set; }
        public StatusViewModel Status { get; set; }
        public IEnumerable<Evidence> Evidence { get; set; }
    }
}