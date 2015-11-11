namespace VTP2015.ServiceLayer.Lecturer.Models
{
    public class RequestPartimInformation
    {
        public int Id { get; set; }
        public Module Module { get; set; }
        public Partim Partim { get; set; }
        public string Argumentation { get; set; }
        public File File { get; set; }
        public System.Collections.Generic.IEnumerable<Evidence> Evidence { get; set; }
    }
}
