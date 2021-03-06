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

        private List<DataModel> models = new List<DataModel>();

        bool isProcessing = false;

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
                Logger.Log("System encountered an unusual exception. Check if your file is in the XML format and make sure it's not corrupted or open etc. \n" + 
                    "Exception Message: " + ex.Message);
                Environment.Exit(0);
            }
        }

        public List<DataModel> GetAllParsedModels()
        {
            return this.models;
        }

        public void Initialize()
        {
            DataModel model = new DataModel();

            Logger.Log("Parsing Initialized. This might take a while.");

            this.xmlReader.MoveToContent();

            while (this.xmlReader.Read())
            {
                if (this.xmlReader.NodeType == XmlNodeType.Element)
                {

                    if (this.isProcessing == false)
                    {
                        model = new DataModel();
                    }

                    if (this.xmlReader.LocalName == "RitStation")
                    {
                        ExtractStationDetails(this.xmlReader.ReadSubtree(), model);
                    }

                    if (this.xmlReader.LocalName == "Trein")
                    {
                        ExtractTrainDetails(this.xmlReader.ReadSubtree(), model);
                    }
                }
            }

            Logger.Log("Parsing Completed.");
            Logger.Log("Parsed a total of " + (models.Count) + " items.");
        }

        private void ExtractStationDetails(XmlReader subReader, DataModel model)
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
                    model.StationCode = savedVars.stationCode;
                    model.LongName = savedVars.longName;

                    this.isProcessing = true;

                    savedVars = (null, null);
                }
            }
        }

        private void ExtractTrainDetails(XmlReader subReader, DataModel model)
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
                        model.Number = savedVars[0];
                        model.Type = savedVars[1];
                        model.OperatingCompany = savedVars[2];
                        model.Destination = savedVars[3];
                        model.PlannedDepartureTime = savedVars[4];
                        model.Delay = savedVars[5];

                        models.Add(model);
                        this.isProcessing = false;
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
