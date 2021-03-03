using System;
using System.Xml;
using System.Xml.Linq;

namespace XMLDataParser
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * TODO:
             * Classes n' Objects
             * .env file
             * Dynamic Filepath
             * Do something with the data
             * 
             */

            const string PATH = "D:/Users/admin/Desktop/University 2021/Projects/XMLDataParser/CleanedXML.xml";

            XmlReader xmlReader = XmlReader.Create(PATH);

            xmlReader.MoveToContent();

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "RitStation")
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("********** " + xmlReader.LocalName + " ***********");
                    ExtractStationDetails(xmlReader.ReadSubtree());
                }

                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Trein")
                {
                    Console.WriteLine("********** " + xmlReader.LocalName + " ***********");
                    ExtractTrainDetails(xmlReader.ReadSubtree());
                }
            }
        }

        private static void ExtractStationDetails(XmlReader subReader)
        {
            while (subReader.Read())
            {
                if (subReader.NodeType == XmlNodeType.Element)
                {
                    switch (subReader.LocalName)
                    {
                        case "StationCode": Console.WriteLine(subReader.LocalName + ": " + subReader.ReadElementContentAsString()); break;
                        case "LangeNaam": Console.WriteLine(subReader.LocalName + ": " + subReader.ReadElementContentAsString()); break;
                    }
                }
            }
        }

        private static void ExtractTrainDetails(XmlReader subReader)
        {
            while (subReader.Read())
            {
                if (subReader.NodeType == XmlNodeType.Element)
                {
                    switch (subReader.LocalName)
                    {
                        case "TreinNummer": Console.WriteLine(subReader.LocalName + ": " + subReader.ReadElementContentAsString()); break;
                        case "TreinSoort": Console.WriteLine(subReader.LocalName + ": " + subReader.ReadElementContentAsString()); break;
                        case "Vervoerder": Console.WriteLine(subReader.LocalName + ": " + subReader.ReadElementContentAsString()); break;
                        case "TreinEindBestemming": ExtractTrainDestination(subReader.ReadSubtree(), subReader.GetAttribute("InfoStatus")); break;
                        case "VertrekTijd": Console.WriteLine(subReader.LocalName + ": " + subReader.ReadElementContentAsString()); break;
                        case "ExacteVertrekVertraging": Console.WriteLine(subReader.LocalName + ": " + subReader.ReadElementContentAsString()); break;
                    }
                }
            }
        }

        private static void ExtractTrainDestination(XmlReader subReader, string _status)
        {
            string status = _status;

            while (subReader.Read())
            {
                if(subReader.NodeType == XmlNodeType.Element && subReader.LocalName == "StationCode")
                {
                    Console.WriteLine(subReader.LocalName + ": " + subReader.ReadElementContentAsString() + " - " + status);
                    break;
                }
            }
        }

    }
}
