using System;
using CsvHelper;

namespace XMLDataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = FileLoader.OpenPathFile();

            if (path == "File path not found.")
            {
                Logger.Log("Please add the full path to your file.");
                path = Console.ReadLine();
                FileLoader.SavePathFile(path);
            }
            else
            {
                Logger.Log("There is already a saved file path. Do you wish to add a different file path?. (Y/N)");

                string changePath = "";

                changePath = Console.ReadLine();

                if (changePath.ToUpper() == "Y")
                {
                    Logger.Log("Please add the full path to your file.");
                    path = Console.ReadLine();
                    FileLoader.SavePathFile(path);
                }
            }

            Parser parser = new Parser(path);

            parser.Initialize();

            Logger.Log("Do you wish to display the parsed data? (Y/N)");

            string viewData = Console.ReadLine();

            if(viewData.ToUpper() == "Y")
            {
                Logger.LogData(parser.GetAllParsedModels());
            }

            Logger.Log("Do you wish to save the parsed data? (Y/N)");

            string saveData = Console.ReadLine();

            if (saveData.ToUpper() == "Y")
            {
                CsvFileExporter csvFileExporter = new CsvFileExporter("D:/Users/admin/Desktop/University 2021/Projects/XMLDataParser/thing.csv", parser.GetAllParsedModels());
                csvFileExporter.ExportCsvFile();
            }

            //D:/Users/admin/Desktop/University 2021/Projects/XMLDataParser/CleanedXML.xml

            //Next up: Save the data in a file/database and do fun stuff. 
        }

    }
}
