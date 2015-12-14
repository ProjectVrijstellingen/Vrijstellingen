using System.Collections;
using System.Collections.Generic;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class Module
    {
        public Module()
        {
            Partims = new List<Partim>();
        }

        public string Name { get; set; }
        public IEnumerable<Partim> Partims { get; set; }

        public void InsertPartim(Partim partim)
        {
            ((List<Partim>) Partims).Add(partim);
        }
    }
}
