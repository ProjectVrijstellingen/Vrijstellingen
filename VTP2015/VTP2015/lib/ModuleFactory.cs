using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.ViewModels.Student;

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
            var module = new Module { ModuleId = Convert.ToInt32(viewModel.ModuleId), Naam = viewModel.ModuleNaam };
            var partim = new Partim { Naam = viewModel.PartimNaam, SuperCode = viewModel.SuperCode};

            if (Modules.All(m => m.ModuleId != module.ModuleId))
            {
                module.Partims.Add(partim);
                Modules.Add(module);
            }
            else
            {
                Modules.First(m => m.ModuleId == module.ModuleId).Partims.Add(partim);
            }
        }
        
    }
}