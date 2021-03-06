using System;

namespace XMLDataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = FileLoader.OpenPathFile();

            if (path == "File path not found.")
            {
                Logger.Log("Please add the full path to the XML file. Including the file name and mime type.");
                path = Console.ReadLine();
                FileLoader.SavePathFile(path);
            }
            else
            {
                Logger.Log("There is already a saved file XML path. Do you wish to add a different file path? (Y/N)");

                string changePath = Console.ReadLine();

                if (changePath.ToUpper() == "Y")
                {
                    Logger.Log("Please add the full path to your XML file.");
                    path = Console.ReadLine();
                    FileLoader.SavePathFile(path);
                }
            }

            Parser parser = new Parser(path);

            parser.Initialize();

            Logger.Log("Do you wish to display the parsed data? (Y/N)");

            string viewData = Console.ReadLine();

            if (viewData.ToUpper() == "Y")
            {
                Logger.LogData(parser.GetAllParsedModels());
            }
            else if (viewData.ToUpper() == "N")
            {
                Logger.Log("Do you wish to save the parsed data? (Y/N)");

                string saveData = Console.ReadLine();

                if (saveData.ToUpper() == "Y")
                {
                    Logger.Log("Name of the file. If a file has the same name it will get overwritten.");
                    string fileName = Console.ReadLine();
                    string filePath = FileLoader.OpenPathFile(true);

                    if(filePath == "File path not found.")
                    {
                        Logger.Log("Please add the full path to the folder where you wish the file to be saved in");
                        filePath = Console.ReadLine();
                        FileLoader.SavePathFile(filePath, true);
                    }
                    else
                    {
                        Logger.Log("There is already a folder where extracted files are saved. Do you wish to change it? (Y/N)");

                        string changePath = Console.ReadLine();

                        if (changePath.ToUpper() == "Y")
                        {
                            Logger.Log("Please add the full path to the folder where you wish the file to be saved in.");
                            filePath = Console.ReadLine();
                            FileLoader.SavePathFile(filePath, true);
                        }
                    }
                   
                    CsvFileExporter csvFileExporter = new CsvFileExporter(filePath, fileName, parser.GetAllParsedModels());
                    csvFileExporter.ExportCsvFile();

                    Logger.Log("Parsing finished. Extracted file has been saved in " + filePath + "\n" + "Thanks for using my parser, I hope it helped.");
                }
                else if(saveData.ToUpper() == "N")
                {
                    Logger.Log("Parsing finished without saving. Thanks for using my parser, I hope it helped.");
                    Environment.Exit(0);
                }



            }
        }

    }
}
