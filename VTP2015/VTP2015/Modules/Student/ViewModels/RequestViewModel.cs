using System.Collections.Generic;

namespace VTP2015.Modules.Student.ViewModels
{
    public class RequestViewModel
    {
        public int FileId { get; set; }
        public string RequestId { get; set; }
        public IEnumerable<int> Evidence { get; set; }
        public IEnumerable<int> Educations { get; set; }
    }
}   