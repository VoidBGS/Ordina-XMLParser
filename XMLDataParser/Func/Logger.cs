using System;
using System.Collections.Generic;
using System.Text;

namespace XMLDataParser
{
    public static class Logger
    {
        public static void LogData(List<DataModel>models)
        {
            Log("**********Initiate Logger**********");

            //"for loops on Lists are a bit more than 2 times cheaper than foreach loops on Lists." <-- In case of big data use for loops, consider even using arrays to be even faster.
            // Article about performance measuring - http://codebetter.com/patricksmacchia/2008/11/19/an-easy-and-efficient-way-to-improve-net-code-performances/
            for (int i = 0; i < models.Count; i++)
            {
                Log("------------------------------");
                Log(models[i].GetInfo());

            }
        }
        public static void Log(string line)
        {
            Console.WriteLine(line);

        }
    }
}
