using System.Collections.Generic;

namespace VTP2015.Modules.Counselor.ViewModels.Models
{
    public class Module
    {
        public string Name { get; set; }
        public IEnumerable<Partim> Partims { get; set; }
    }
}