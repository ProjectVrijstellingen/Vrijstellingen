using System;

namespace VTP2015.Modules.Counselor.ViewModels
{
    public class FileOverviewViewModel
    {
        public string Id { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentName { get; set; }

        public int AmountOfRequestsOpen
        {
            get
            {
                return AmountOfRequests - (AmountOfRequests * PercentageOfRequestsDone / 100);
            }
        }

        public int AmountOfRequests { get; set; }
        public int PercentageOfRequestsDone { get; set; }
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
                if (PercentageOfRequestsDone == 0)
                {
                    color = "danger";
                }
                else if (PercentageOfRequestsDone == 100)
                {
                    color = "success";
                }
                return color;
            }
        }
    }
}