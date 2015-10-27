using System.Collections.Generic;

namespace VTP2015.lib
{
    public class Module
    {
        private List<Partim> _partims = new List<Partim>();

        public int ModuleId { get; set; }

        public List<Partim> Partims
        {
            get { return _partims; }
            set { _partims = value; }
        }

        public string Name { get; set; }

    }
}