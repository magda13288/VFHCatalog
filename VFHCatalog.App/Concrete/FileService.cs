using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App.Abstract;
using Newtonsoft.Json;
using System.IO;
using VFHCatalog.Domain.Entity;
using VFHCatalog.App.Managers;
using VFHCatalog.Domain.Common;
using System.Xml.Serialization;
using VFHCatalog.App.Helpers;
using System.Xml.Linq;
using System.Linq;
using VFHCatalog;

namespace VFHCatalog.App.Concrete
{
    public class FileService : IFileService
    {
        private string _path;
        private PlantService _plantService;
        private ErrorsService _errorService;
        private readonly string _errorPath;
        private Error error = new Error();
        private readonly string path = Directory.GetCurrentDirectory() + "\\FileWriterErrors\\Errors.txt";
        string filePath;
        
        MessagesService message = new MessagesService();
        //ErrorsService errorServiceTemp = new ErrorsService();

        public FileService()
        { }
        public FileService(PlantService plantService, string path, ErrorsService errorsService, string errorPath)
        {   
            _plantService = plantService;
            _path = path;
            _errorService = errorsService;
            _errorPath = errorPath;    
        }
        public FileService(ErrorsService errorService, string errorPath)
        {
            _errorService = errorService;
            _errorPath = errorPath;
        }
            
        public void FileJSONReader()
        {       
            try
            {        
                if (File.Exists(_path))
                {
                    using StreamReader sr = new StreamReader(_path);
                    string output = sr.ReadToEnd();
                    output = output.Substring(1, output.Length - 2);
                    string subOtput = output.Replace("\\", "");
                    var plantsFromJSON = JsonConvert.DeserializeObject<PlantService>(subOtput);
                    sr.Close();

                    for (int i = 0; i < plantsFromJSON.Items.Count; i++)
                    {
                        _plantService.AddItem(plantsFromJSON.Items[i]);
                    }
                }
            }
            catch (Exception e)
            {
                _errorService.CatchError(e, _errorService);
                //FileErrorsJSONWriter();
            }
        }
        public void FileErrorJSONReader()
        {
            try
            {
                if (File.Exists(_errorPath))
                {
                    using StreamReader sr = new StreamReader(_errorPath);
                    string output = sr.ReadToEnd();
                    output = output.Substring(1, output.Length - 2);
                    string subOtput = output.Replace("\\", "");
                    var errorsFromJSON = JsonConvert.DeserializeObject<ErrorsService>(subOtput);
                    sr.Close();

                    for (int i = 0; i < errorsFromJSON.Items.Count; i++)
                    {
                        _errorService.AddItem(errorsFromJSON.Items[i]);
                    }
                }
            }
            catch (Exception e)
            {
                _errorService.CatchError(e, _errorService);
               // FileErrorsJSONWriter();
            }
        }
        public void FileJSONWriter()
        {
            // public void FileJSONWriter<T>(T service)
             try
                {
                //var typeOfService = service.GetType();

                //if (typeOfService.GetType() == _plantService.GetType() )
                //{
                //    pathJsonWriter = _path;

                //}
                //else
                //    if (typeOfService == _errorService.GetType())
                //{
                //    pathJsonWriter = _errorPath;
                //}
                string output = JsonConvert.SerializeObject(_plantService);

                    if (output != null)
                    {
                        using StreamWriter sw = new StreamWriter(_path);
                        using JsonWriter writer = new JsonTextWriter(sw);;
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(writer, output);
                        sw.Close();
                        writer.Close();
                    }
                }
            catch (Exception e)
            {
               error= _errorService.CatchError(e, _errorService);
                SaveErrorFromErrorWriter(error, _errorPath); 

            }
        }

        public void FileErrorsJSONWriter()
        {
            try
            {
                string output = JsonConvert.SerializeObject(_errorService);

                if (output != null)
                {
                    using StreamWriter sw = new StreamWriter(_errorPath);
                    using JsonWriter writer = new JsonTextWriter(sw); ;
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, output);
                    sw.Close();
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                error =_errorService.CatchError(e, _errorService);
                SaveErrorFromErrorWriter(error,_errorPath);

            }

        }        
        public void FileXMLReader()
        {

            if (File.Exists(_path))
            {
                try
                {
                    string output = File.ReadAllText(_path);

                    if (output != null)
                    {
                        StringReader sr = new StringReader(output);

                        XmlRootAttribute root = new XmlRootAttribute();
                        root.ElementName = "Plants";
                        root.IsNullable = true;

                        XmlSerializer serializer = new XmlSerializer(typeof(PlantService), root);
                        var plantsFromXML = (PlantService)serializer.Deserialize(sr);
                        sr.Close();

                        for (int i = 0; i < plantsFromXML.Items.Count; i++)
                        {
                            _plantService.AddItem(plantsFromXML.Items[i]);
                        }                       
                    }
                }
                catch (Exception e)
                {
                    _errorService.CatchError(e,_errorService);
                    //FileErrorsXMLWriter();
               }
            }
        }

        public void FileErrorXMLReader()
        {
            if (File.Exists(_errorPath))
            {
                try
                {
                    string output = File.ReadAllText(_errorPath);

                    if (output != null)
                    {
                        StringReader sr = new StringReader(output);

                        XmlRootAttribute root = new XmlRootAttribute();
                        root.ElementName = "Errors";
                        root.IsNullable = true;

                        XmlSerializer serializer = new XmlSerializer(typeof(ErrorsService), root);
                        var ErrorsFromXML = (ErrorsService)serializer.Deserialize(sr);
                        sr.Close();

                        for (int i = 0; i < ErrorsFromXML.Items.Count; i++)
                        {
                            _errorService.AddItem(ErrorsFromXML.Items[i]);
                        }
                    }
                }
                catch (Exception e)
                {
                    _errorService.CatchError(e, _errorService);
                    //FileErrorsXMLWriter();
                }
            }
        }
        public void FileXMLWriter(PlantService plantService)
        { 
                if (File.Exists(_path))
               {
                if (plantService.Items.Count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Bye bye:)");
                    Console.WriteLine();
                }
                else
                {
                    try
                    {

                        XElement xmlPlant = XElement.Load(_path);

                        for (int i = 0; i < plantService.Items.Count; i++)
                        {
                            xmlPlant.Add(new XElement("Items", new XElement("Plant", new XAttribute("Id", plantService.Items[i].Id.ToString()),
                                      new XElement("IdType", plantService.Items[i].IdType.ToString()),
                                      new XElement("GroupId", plantService.Items[i].GroupId.ToString()),
                                      new XElement("NameId", plantService.Items[i].NameId.ToString()),
                                      new XElement("FullName", plantService.Items[i].FullName),
                                      new XElement("Type", plantService.Items[i].Type),
                                      new XElement("Color", plantService.Items[i].Color),
                                      new XElement("Destination", plantService.Items[i].Destination),
                                      new XElement("Description", plantService.Items[i].Description),
                                      new XElement("Opinion", plantService.Items[i].Opinion))));

                            xmlPlant.Save(_path);
                        }
                    }
                    catch (Exception e)
                    {
                        _errorService.CatchError(e, _errorService);
                      // FileErrorsXMLWriter();

                    }
                }
            }
            else
            {
                try
                {
                    XmlRootAttribute root = new XmlRootAttribute();
                    root.ElementName = "Plants";
                    root.IsNullable = true;
                    XmlSerializer serializer = new XmlSerializer(typeof(PlantService), root);
                    using StreamWriter sw = new StreamWriter(_path, true);

                    serializer.Serialize(sw, _plantService);
                    sw.Close();
                }
                catch (Exception e)
                {
                    _errorService.CatchError(e, _errorService);
                   // FileErrorsXMLWriter();

                }
            }
        }   
        public void FileErrorsXMLWriter()
        {
                if (File.Exists(_errorPath))
                {
                    try
                    {
                        XElement xmlPlant = XElement.Load(_errorPath);
                
                        for (int i = 0; i < _errorService.Items.Count; i++)
                        {
                            xmlPlant.Add(new XElement("Items", new XElement("Error", new XAttribute("Id",_errorService.Items[i].Id.ToString()),
                                      new XElement("Message", _errorService.Items[i].Message),
                                      new XElement("Source", _errorService.Items[i].Source),
                                      new XElement("StackTrace", _errorService.Items[i].StackTrace),
                                      //new XElement("InnerException", _errorService.Items[i].InnerException),
                                      new XElement("DateTime", _errorService.Items[i].DateTime))));

                            xmlPlant.Save(_errorPath);
            
                        }
                    }

                    catch (Exception e)
                    {
                    error = _errorService.CatchError(e, _errorService);
                    SaveErrorFromErrorWriter(error, path);

                    }
                }
                else
                {
                    try
                    {
                        XmlRootAttribute root = new XmlRootAttribute();
                        root.ElementName = "Errors";
                        root.IsNullable = true;
                        XmlSerializer serializer = new XmlSerializer(typeof(ErrorsService), root);
                        using StreamWriter sw = new StreamWriter(_errorPath, true);

                        serializer.Serialize(sw, _errorService);
                        sw.Close();
                    }
                    catch (Exception e)
                    {
                    error = _errorService.CatchError(e, _errorService);
                    SaveErrorFromErrorWriter(error, path);
                    }

                }
            }
        public void SaveErrorFromErrorWriter(Error error, string path)
        {
            if (File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"{error.DateTime.ToString()}");
                    sw.WriteLine(error.Message);
                    sw.WriteLine(error.StackTrace);
                    sw.WriteLine(error.Source);
                    //sw.WriteLine(error.InnerException);
                    sw.WriteLine();
                }
            }
            else
            {
                FileStream  fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter( fs))
                {
                    sw.WriteLine($"{error.DateTime.ToString()}");
                    sw.WriteLine(error.Message);
                    sw.WriteLine(error.StackTrace);
                    sw.WriteLine(error.Source);
                    //sw.WriteLine(error.InnerException);
                    sw.WriteLine();
                }
                 fs.Close();
            }         
        }

        public void DeleteItemFromXMLFile(string id, int option)
        {
            string desc = default;


            if (option == 1)
            {
                desc = "Plant";
                filePath = _path;
            }
            else if (option == 2)
            {
                desc = "Error";
                filePath = _errorPath;
            }

            try
            {           
                if (File.Exists(filePath))
                {
                    XDocument xmlDoc = XDocument.Load(filePath);

                    xmlDoc.Descendants(desc).Where(p => p.Attribute("Id").Value == id).FirstOrDefault().Remove();

                    xmlDoc.Save(filePath);
                   
                }
            }
            catch (Exception e)
            {
                _errorService.CatchError(e, _errorService);
               // FileErrorsXMLWriter();
            }
        }
        public void EditPlantInXMLFile(Plant plant)
        { 
            if (File.Exists(_path))
            {
                try
                {
                    XDocument xmlDoc = XDocument.Load(_path);

                    var xmlPlant = xmlDoc.Root.Descendants("Plant").Where(p => p.Attribute("Id").Value == plant.Id.ToString()).FirstOrDefault();

                    xmlPlant.Element("FullName").Value = plant.FullName;
                    xmlPlant.Element("Type").Value= plant.Type;
                    xmlPlant.Element("Color").Value= plant.Color;
                    xmlPlant.Element("Destination").Value =  plant.Destination;
                    xmlPlant.Element("Description").Value = plant.Description;
                    xmlPlant.Element("Opinion").Value= plant.Opinion;

                    xmlDoc.Save(_path);
                    
                }
                catch (Exception e)
                {
                    _errorService.CatchError(e, _errorService);
                   // FileErrorsXMLWriter();

                }
            }
        }
    }

}
