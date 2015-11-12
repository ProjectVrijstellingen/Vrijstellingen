using System;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class FileOverviewViewModel
    {
        public string Id { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentName { get; set; }
        public int AmountOfRequestsOpen { get; set; }
        public int PercentageOfRequestsOpen { get; set; }
        public string Route { get; set; }
        public string AcademicYear { get; set; }
        public DateTime DateCreated { get; set; }
    }
}