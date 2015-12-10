using System.Collections.Generic;
using VTP2015.Modules.Counselor.ViewModels.Models;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class RequestDetailViewModel
    {
        public string StudentName { get; set; }
        public IEnumerable<Module> Modules { get; set; }
    }
}