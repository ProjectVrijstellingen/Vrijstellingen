using System.Collections.Generic;
using VTP2015.lib;
using VTP2015.Modules.Shared;

namespace VTP2015.Modules.Student.ViewModels
{
    public class FileViewModel : IViewModel
    {
        public List<Module> RequestedModules { get; set; }
        public List<Module> AvailableModules { get; set; }
    }
}