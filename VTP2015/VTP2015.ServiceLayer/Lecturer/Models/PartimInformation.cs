using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTP2015.ServiceLayer.Lecturer.Models
{
    public class PartimInformation
    {
        public string SuperCode { get; set; }
        public int PartimId { get; set; }
        public int ModuleId { get; set; }
        public string PartimName { get; set; }
        public string ModuleName { get; set; }
        //public virtual Partim Partim { get; set; }
        //public virtual Module Module { get; set; }
        //public virtual Lecturer Lecturer { get; set; }
        //public virtual Route Route { get; set; }
        //public virtual ICollection<RequestPartimInformation> RequestPartimInformations { get; set; }
    }
}
