namespace VTP2015.ServiceLayer.Student.Models
{
    public class PartimInformation
    {
        public string Code { get; set; }
        public string SuperCode { get; set; }
        public int Semester { get; set; }
        public string ModuleName { get; set; }
        public string PartimName { get; set; }
        public int RequestCount { get; set; }
        public int TotalCount { get; set; }
        public Status Status { get; set; }
    }
}
