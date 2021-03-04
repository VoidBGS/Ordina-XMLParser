using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace XMLDataParser
{
    public static class FileLoader
    {
        static string PATH = "../netcoreapp3.1/Path.txt";

        public static string OpenPathFile()
        {
            try
            {
                return System.IO.File.ReadAllText(PATH);
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
