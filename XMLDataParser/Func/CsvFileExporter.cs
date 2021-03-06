using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace XMLDataParser
{
    public class CsvFileExporter
    {
        private string filePath = "";

        private string fileName = "";

        private List<DataModel> models;

        public CsvFileExporter(string filePath, string fileName, List<DataModel> models)
        {
            this.filePath = filePath;
            this.fileName = fileName;
            this.models = models;
        }

        public void ExportCsvFile()
        {
                WriteToFile(models);
        }

        private void WriteToFile(IEnumerable records)
        {
            try
            {
                using (var writer = new StreamWriter(filePath + "/" + fileName + ".csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);

                    writer.Flush();
                }
            }
            catch(DirectoryNotFoundException)
            {
                Logger.Log("The specified directory could not be found. Make sure that the path is correct. " + "\n" + "Specified path - " + filePath);
                FileLoader.DeletePathFile(true);
                Environment.Exit(0);
            }
            catch (PathTooLongException)
            {
                Logger.Log("File path was too long. Please move the file and try again.");
                FileLoader.DeletePathFile();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Logger.Log("System encountered an unusual exception. Check if you file is in the XML format and make sure it's not corrupted or in use etc. \n" +
                "Exception Message: " + ex.Message);
                FileLoader.DeletePathFile(true);
                Environment.Exit(0);
            }

        }
    }
}
