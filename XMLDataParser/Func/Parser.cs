using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XMLDataParser
{
    public class Parser
    {
        private XmlReader xmlReader;

        private List<Station> stations = new List<Station>();

        private List<Train> trains = new List<Train>();

        public Parser(string filePath)
        {
            try
            {
                this.xmlReader = XmlReader.Create(filePath);
            }
            catch (FileNotFoundException)
            {
                Logger.Log("File path was set incorrectly. Please try again.");
                FileLoader.DeletePathFile();
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
                Logger.Log(ex.Message);
                Environment.Exit(0);
            }
        }

        public List<Station> GetAllParsedStations()
        {
            return this.stations;
        }

        public List<Train> GetAllParsedTrains()
        {
            return this.trains;
        }

        public void Initialize()
        {
            Logger.Log("Parsing Initialized. This might take a while.");

            this.xmlReader.MoveToContent();

            while (this.xmlReader.Read())
            {
                if (this.xmlReader.NodeType == XmlNodeType.Element && this.xmlReader.LocalName == "RitStation")
                {
                    ExtractStationDetails(this.xmlReader.ReadSubtree());
                }

                if (this.xmlReader.NodeType == XmlNodeType.Element && this.xmlReader.LocalName == "Trein")
                {
                    ExtractTrainDetails(this.xmlReader.ReadSubtree());
                }
            }

            Logger.Log("Parsing Completed.");
            Logger.Log("Parsed a total of " + (trains.Count + stations.Count) + " items.");
        }

        private void ExtractStationDetails(XmlReader subReader)
        {
            (string stationCode, string longName) savedVars = (null, null);

            while (subReader.Read())
            {
                if (subReader.NodeType == XmlNodeType.Element)
                {
                    switch (subReader.LocalName)
                    {
                        case "StationCode":
                            savedVars.stationCode = subReader.ReadElementContentAsString();
                            break;
                        case "LangeNaam":
                            savedVars.longName = subReader.ReadElementContentAsString();
                            break;
                    }
                }

                if(!String.IsNullOrEmpty(savedVars.longName) && !String.IsNullOrEmpty(savedVars.stationCode))
                {
                    Station station = new Station();

                    station.StationCode = savedVars.stationCode;
                    station.LongName = savedVars.longName;

                    stations.Add(station);
                    savedVars = (null, null);
                }
            }
        }

        private void ExtractTrainDetails(XmlReader subReader)
        {
            string[] savedVars = new string[6];
            int hit = 0;

            while (subReader.Read())
            {
                if (subReader.NodeType == XmlNodeType.Element)
                {
                    switch (subReader.LocalName)
                    {
                        case "TreinNummer":
                            hit++;
                            savedVars[0] = subReader.ReadElementContentAsString();
                            // ReadElementContentAsString moves the pointer to the next element, so it goes to TreinSoort as that comes right after TreinNummer.
                            savedVars[1] = subReader.ReadElementContentAsString(); 
                            break;
                        case "Vervoerder":
                            hit++;
                            savedVars[2] = subReader.ReadElementContentAsString();
                            break;
                        case "TreinEindBestemming":
                            hit++;
                            //Goes in this 2 times, and overwrites savedVars[3]
                            savedVars[3] = ExtractTrainDestination(subReader.ReadSubtree());
                            break;
                        case "VertrekTijd":
                            hit++;
                            savedVars[4] = subReader.ReadElementContentAsString();
                            break;
                        case "ExacteVertrekVertraging":
                            hit++;
                            savedVars[5] = subReader.ReadElementContentAsString();
                            break;
                    }

                    if(hit > 5)
                    {
                        Train train = new Train();
                        train.Number = savedVars[0];
                        train.Type = savedVars[1];
                        train.OperatingCompany = savedVars[2];
                        train.Destination = savedVars[3];
                        train.PlannedDepartureTime = savedVars[4];
                        train.Delay = savedVars[5];

                        trains.Add(train);
                        hit = 0;
                    }

                }
            }
        }

        private string ExtractTrainDestination(XmlReader subReader)
        {
            while (subReader.Read())
            {
                if (subReader.NodeType == XmlNodeType.Element && subReader.LocalName == "StationCode")
                {
                    return subReader.ReadElementContentAsString();
                }
            }

            return null;
        }
    }
}
