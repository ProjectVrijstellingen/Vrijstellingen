using System.Collections;
using System.Collections.Generic;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class Module
    {
        public string Name { get; set; }
        public IEnumerable<Partim> Partims { get; set; }
    }
}
