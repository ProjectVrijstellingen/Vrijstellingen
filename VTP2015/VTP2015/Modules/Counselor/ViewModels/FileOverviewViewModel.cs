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

        public int DaysRemaining
        {
            get
            {
                var daysLeft = 21 - (DateTime.Today - DateCreated).Days;
                return daysLeft > 0 ? daysLeft : 0;
            }
        }

        public string Color
        {
            get
            {
                var color = "warning";
                if (PercentageOfRequestsOpen == 100) { color = "danger"; }
                else if (PercentageOfRequestsOpen == 0) { color = "success"; }
                return color;
            }
        }
    }
}