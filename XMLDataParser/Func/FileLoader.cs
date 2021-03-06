using System;
using System.IO;

namespace XMLDataParser
{
    public static class FileLoader
    {
        static string PATH = "../netcoreapp3.1/Path.txt";
        static string EXPORT = "../netcoreapp3.1/ExportPath.txt";

        public static string OpenPathFile(bool isExport = false)
        {
            try
            {
                if (isExport == false)
                {
                    return File.ReadAllText(PATH);
                }
                else
                {
                    return File.ReadAllText(EXPORT);
                }
            }
            catch(FileNotFoundException)
            {
                return "File path not found.";
            }
        }

        public static void SavePathFile(string filePath, bool isExport = false)
        {
            try
            {
                if (isExport == false)
                {
                    File.WriteAllText("Path.txt", filePath);
                }
                else
                {
                    File.WriteAllText("ExportPath.txt", filePath);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Environment.Exit(0);
            }
        }

        public static void DeletePathFile(bool isExport = false)
        {
            try
            {
                if (isExport == false)
                {
                    File.Delete(PATH);
                }
                else
                {
                    File.Delete(EXPORT);
                }
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message);
                Environment.Exit(0); 
            }
        }
    }
}
