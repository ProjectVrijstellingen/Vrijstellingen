using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.Modules.Student.ViewModels;

namespace VTP2015.lib
{
    public class ModuleFactory
    {
        public ModuleFactory(PartimViewModel[] viewModels)
        {
            Modules = new List<Module>();
            AddPartimsToLists(viewModels);
        }

        public List<Module> Modules { get; set; }

        private void AddPartimsToLists(PartimViewModel[] viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                AddPartim(viewModel);
            }
        }

        private void AddPartim(PartimViewModel viewModel)
        {
            var module = new Module { Code = viewModel.Code, Name = viewModel.ModuleName };
            var partim = new Partim { Name = viewModel.PartimName, SuperCode = viewModel.SuperCode};

            if (Modules.All(m => m.Code != module.Code))
            {
                module.Partims.Add(partim);
                Modules.Add(module);
            }
            else
            {
                Modules.First(m => m.Code == module.Code).Partims.Add(partim);
            }
        }
        
    }
}