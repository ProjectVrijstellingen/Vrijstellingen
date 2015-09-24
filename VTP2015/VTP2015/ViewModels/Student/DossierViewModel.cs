using System;
using System.Collections.Generic;
using VTP2015.lib;

namespace VTP2015.ViewModels.Student
{
    public class DossierViewModel : IViewModel
    {
        public List<Module> AangevraagdeModules { get; set; }
        public List<Module> BeschikbareModules { get; set; }
    }
}