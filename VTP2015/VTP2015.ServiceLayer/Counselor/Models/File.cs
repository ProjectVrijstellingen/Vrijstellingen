using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace VTP2015.ServiceLayer.Counselor.Models
{
    public class File
    {
        public File()
        {
            Modules = new List<Module>();
        }

        public int Id { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentName { get; set; }
        public string StudentMail { get; set; }
        public int AmountOfRequests { get; set; }
        public int PercentageOfRequestsDone { get; set; }
        public string Route { get; set; }
        public string AcademicYear { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<Module> Modules { get; set; }

        public int AmountOfApprovedRequests => CountRequests(Status.Approved);
        public int AmountOfDeniedRequests => CountRequests(Status.Rejected);
        public int AmountOfUntreatedRequests => CountRequests(Status.Untreated);

        private int CountRequests(Status status)
        {
            return Modules.SelectMany(m => m.Partims).Count(p => p.Status == status);
        }

        public void InsertModule(Module module)
        {
            if (Modules.All(m => m.Name != module.Name))
                ((List<Module>)Modules).Add(module);
        }

        public void InsertPartim(Partim partim, string moduleName)
        {
            var module = Modules.First(m => m.Name == moduleName);
            module.InsertPartim(partim);
        }
    }
}
