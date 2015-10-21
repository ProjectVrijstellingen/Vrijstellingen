using System.Linq;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string FileId { get; set; }
        public Status Status { get; set; }
        public string Argumentation { get; set; }
        public string PartimName { get; set; }
        public string ModuleName { get; set; }
        public IQueryable<Evidence> Evidence { get; set; }
    }
}
