using System;
using System.Collections.Generic;
using System.Text;

namespace XMLDataParser
{
    public class DataModel
    {
        public string StationCode { get; set; }
        public string LongName { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string OperatingCompany { get; set; }
        public string Destination { get; set; }
        public string PlannedDepartureTime { get; set; }
        public string Delay { get; set; }

        public string GetInfo()
        {
            string info =
                "**********Station *********** " + "\n" +
                "Code: " + this.StationCode + "\n" +
                "Name: " + this.LongName + "\n" +
                "*********** Train ************" + "\n" +
                "Number: " + this.Number + "\n" +
                "Type: " + this.Type + "\n" +
                "OperatingCompany: " + this.OperatingCompany + "\n" +
                "Destination: " + this.Destination + "\n" +
                "PlannedDepartureTime: " + this.PlannedDepartureTime + "\n" +
                "Delay: " + this.Delay;

            return info;
        }
 
    }
}
