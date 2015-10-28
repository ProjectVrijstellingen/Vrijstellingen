using System.Collections.Generic;

namespace VTP2015.Modules.Student.ViewModels
{
    public class RequestPartimInformationViewModel
    {
        public int RequestId { get; set; }
        public string Status { get; set; }
        public PartimViewModel Partim { get; set; }
    }
}