using System.Collections.Generic;

namespace Web_Application.Models
{
    public class ComputersAndHallsViewModel
    {
        public IEnumerable<Computers> Computers { get; set; }
        public IEnumerable<Halls> Halls { get; set; }

    }
}
