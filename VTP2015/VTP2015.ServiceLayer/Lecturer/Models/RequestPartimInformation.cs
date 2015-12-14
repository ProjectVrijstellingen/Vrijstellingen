using System.Linq;

namespace VTP2015.ServiceLayer.Lecturer.Models
{
    public class RequestPartimInformation
    {
        public RequestPartimInformation()
        {
            Status = Status.Untreated;
        }
        public Status Status { get; set; }
        public int Id { get; set; }
        public Partim Partim { get; set; }
        public Module Module { get; set; }
        public string Argumentation { get; set; }
        public IQueryable<Evidence> Evidence { get; set; }
        public Student Student { get; set; }
        public string SuperCode { get; set; }
        public Motivation Motivation { get; set; }
        //public virtual Request Request { get; set; }
        //public virtual PartimInformation PartimInformation { get; set; }
    }
}
