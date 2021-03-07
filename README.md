# Ordina-XMLParser
Custom file parser made for extracting data from Ordina XML files. Made in Visual Studio using XmlReader in C#.

## Features
>**XML File Parser** - Working file parser that extracts specific data from cleaned Ordina XML files. The extracted information is stored in a CSV File with matching columns.

>**Preview Data Mode** - Before saving the data into a CSV file there is an option to preview it and quickly check for mistakes.

>**Dynamic File Path Saving** - Saves your added path to both your export folder and XML file. This is added so the user can quickly go through the parsing process, without having to enter in a new file path everytime.

>**Exact Data Extraction** - The Ordina-XMLParser extracts only the most important specified data and not the whole XML file, so you won't need to bother cleaning the entire data.

### Extracted Data Example
![Extracte Data Picture](https://i.ibb.co/d01XZbq/Screenshot-21.png)

## Developer Note
This file parser will not work on any other XML files except the ones given to us by Ordina. It is ***very specific*** to the usecase we currently have.

**It is expected that the initial CSV files from Ordina are parsed to XML. This can easily be done using Excel.**
