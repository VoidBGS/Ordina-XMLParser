using System;
using System.Collections.Generic;
using System.Text;

namespace XMLDataParser
{
    public class Train
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public string OperatingCompany { get; set; }
        public string Destination { get; set; }
        public string PlannedDepartureTime { get; set; }
        public string Delay { get; set; }
    }
}
