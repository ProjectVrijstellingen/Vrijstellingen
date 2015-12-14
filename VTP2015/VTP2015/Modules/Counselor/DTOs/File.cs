using System.Collections.Generic;

namespace VTP2015.Modules.Counselor.DTOs
{
    public class File
    {
        public string StudentName { get; set; }
        public IEnumerable<Module> Modules { get; set; } 
    }
}