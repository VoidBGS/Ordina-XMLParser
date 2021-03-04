using System;

namespace XMLDataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * TODO:
             * Dynamic Filepath
             * Do something with the data
             * 
             */

            const string PATH = "D:/Users/admin/Desktop/University 2021/Projects/XMLDataParser/CleanedXML.xml";

            Parser parser = new Parser(PATH);

            parser.Initialize();

            Logger.Log("Do you wish to display the parsed data? (Y/N)");

            string viewData = Console.ReadLine();

            if(viewData.ToUpper() == "Y")
            {
                Logger.LogData(parser.GetAllParsedTrains(), parser.GetAllParsedStations());
            }

            Logger.Log("Do you wish to save the parsed data? (Y/N)");

            string saveData = Console.ReadLine();

            Logger.Log(saveData);

        }

    }
}
