using System.Collections;
using System.Collections.Generic;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class Partim
    {
        public string Name { get; set; }
        public IEnumerable<Evidence> Evidence { get; set; }
    }
}
