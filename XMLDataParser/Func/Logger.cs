using System;
using System.Collections.Generic;
using System.Text;

namespace XMLDataParser
{
    public static class Logger
    {
        public static void LogData(List<Train>trains, List<Station> stations)
        {
            Log("**********Initiate Logger**********");

            //"for loops on Lists are a bit more than 2 times cheaper than foreach loops on Lists." <-- In case of big data use for loops, consider even using arrays to be even faster.
            // Article about performance measuring - http://codebetter.com/patricksmacchia/2008/11/19/an-easy-and-efficient-way-to-improve-net-code-performances/
            for (int i = 0; i < stations.Count; i++)
            {
                Log("------------------------------");
                Log("********** Station ***********");
                Log("Code: " + stations[i].StationCode + "\n" +
                    "Name: " + stations[i].LongName);
                Log("*********** Train ************");
                Log("Number: " + trains[i].Number + "\n" +
                    "Type: " + trains[i].Type + "\n" +
                    "OperatingCompany: " + trains[i].OperatingCompany + "\n" +
                    "Destination: " + trains[i].Destination + "\n" +
                    "PlannedDepartureTime: " + trains[i].PlannedDepartureTime + "\n" +
                    "Delay: " + trains[i].Delay);

            }
        }
        public static void Log(string line)
        {
            Console.WriteLine(line);

        }
    }
}
