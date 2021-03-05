using System;
using System.IO;

namespace XMLDataParser
{
    public static class FileLoader
    {
        static string PATH = "../netcoreapp3.1/Path.txt";

        public static string OpenPathFile()
        {
            try
            {
                return File.ReadAllText(PATH);
            }
            catch(FileNotFoundException)
            {
                return "File path not found.";
            }
        }

        public static void SavePathFile(string filePath)
        {
            try
            {
                File.WriteAllText("Path.txt", filePath);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Environment.Exit(0);
            }
        }
        
        public static void DeletePathFile()
        {
            try
            {
                File.Delete(PATH);
            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message);
                Environment.Exit(0); 
            }
        }
    }
}
