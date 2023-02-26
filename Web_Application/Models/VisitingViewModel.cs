using System;

namespace Web_Application.Models
{
    public class VisitingViewModel
    {
        public int VisitingCode { get; set; }
        public int VisititorCode { get; set; }
        public string VisitorName { get; set; }
        public string ServicesName { get; set; }
        public DateTime DateOfVisit { get; set; }
        public DateTime VisitTime { get; set; }
        public int NumberOfHour { get; set; }
        public string HallsName { get; set; }
        public string ComputerName { get; set; }
        public int ComputersCode { get; set; }
        public Computers Computers { get; set; }
        public string ApplicationUserName { get; set; }

        public bool IsComputerSelected { get; set; }

    }
}
