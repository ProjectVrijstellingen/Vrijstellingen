using System;
using System.Linq;

namespace VTP2015.ServiceLayer.Student.Models
{
    public class Request
    {
        public string Argumentation { get; set; }
        public DateTime LastChanged { get; set; }
        public Status Status { get; set; }
        public int PartimInformationId { get; set; }
        public int FileId { get; set; }
        public string PartimInformationSuperCode { get; set; }
        public IQueryable<Evidence> Evidence { get; set; }
    }
}
