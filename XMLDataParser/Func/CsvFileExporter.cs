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

        private List<DataModel> models;

        public CsvFileExporter(string filePath, List<DataModel> models)
        {
            this.filePath = filePath;
            this.models = models;
        }

        public void ExportCsvFile()
        {
                WriteToFile(models);
        }

        private void WriteToFile(IEnumerable records)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);

                writer.Flush();
            }

        }
    }
}
