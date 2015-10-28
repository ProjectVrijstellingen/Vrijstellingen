using System;
using System.Linq;

namespace VTP2015.ServiceLayer.Student.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string Name { get; set; }
        public string Argumentation { get; set; }
        public DateTime LastChanged { get; set; }
        public int FileId { get; set; }
        public IQueryable<Evidence> Evidence { get; set; }
        public IQueryable<int> PartimInformationIds { get; set; } 
    }
}
