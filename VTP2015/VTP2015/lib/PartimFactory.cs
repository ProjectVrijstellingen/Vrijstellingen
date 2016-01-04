using System.Collections.Generic;
using System.Linq;
using VTP2015.Modules.Student.ViewModels;

namespace VTP2015.lib
{

    //TODO: Uitleg
    public class PartimFactory
    {
        public PartimFactory(PartimViewModel[] viewModels)
        {
            Semesters = new List<Semester>();
            AddPartimsToLists(viewModels);
        }
         
        public List<Semester> Semesters { get; set; }

        private void AddPartimsToLists(PartimViewModel[] viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                AddPartim(viewModel);
            }
        }

        private void AddPartim(PartimViewModel viewModel)
        {
            var partim = new Partim { Name = viewModel.PartimName, SuperCode = viewModel.SuperCode, Status = viewModel.Status};

            if (Semesters.All(s => s.Number != viewModel.Semester))
                Semesters.Add(new Semester {Number = viewModel.Semester});

            var semester = Semesters.First(s => s.Number == viewModel.Semester);

            if (semester.Modules.All(m => m.Code != viewModel.Code))
                semester.Modules.Add(new Module
                {
                    Code = viewModel.Code,
                    Name = viewModel.ModuleName,
                    RequestCount = viewModel.RequestCount,
                    TotalCount = viewModel.TotalCount
                });
            semester.Modules.First(m => m.Code == viewModel.Code).Partims.Add(partim);
        }
        
    }
}