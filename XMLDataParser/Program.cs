using System;

namespace XMLDataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * TODO:
             * Do something with the data
             * 
             */

            string PATH = FileLoader.OpenPathFile();

            if(PATH == "File path not found.")
            {
                Logger.Log("Please add the full path to your file.");
                PATH = Console.ReadLine();
                FileLoader.SavePathFile(PATH);
            }

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

            //Next up: Save the data in a file/database and do fun stuff. 
        }

    }
}
