using System.Collections.Generic;

namespace VTP2015.lib
{
    public class Semester
    {
        private List<Module> _modules = new List<Module>();
        public int Number { get; set; }

        public List<Module> Modules
        {
            get { return _modules; }
            set { _modules = value; }
        }
    }
}