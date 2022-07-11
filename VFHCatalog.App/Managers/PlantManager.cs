using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App.Concrete;
using VFHCatalog.App.Helpers;
using VFHCatalog.Domain.Entity;
using System.Text.RegularExpressions;
using VFHCatalog.App.Common;

namespace VFHCatalog.App.Managers
{
    public class PlantManager : Plant
    {
        private readonly MenuActionService _actionService;
        private PlantService _plantService;
        private ErrorsService _errorService;
        private readonly string _errorPath;
        private FileService _fileService;


        Error error = new Error();
        MessagesService message = new MessagesService();
        DataValidation data = new DataValidation();

        int keyId, groupId, nameId;
        string name, outName;
        ConsoleKeyInfo readKeyItemGroupMenu, readKeyItemName;

        public PlantManager()
        { }
        public PlantManager(MenuActionService actionService, PlantService plantService,ErrorsService errorsService, string errorPath, FileService fileService)
        {
            _plantService = plantService;
            _actionService = actionService;
            _errorService = errorsService;
            _errorPath = errorPath;
            _fileService = fileService;
                 
        }
        public void AddPlant(string menuName, string key, string messageWhat, string messageName)
        {
            try
            {
                keyId = int.Parse(key);

                Console.WriteLine();
                //GroupId /psianka,dyniowate itd
                readKeyItemGroupMenu = _actionService.LoadMenu(messageWhat, menuName, out name);
                Console.WriteLine();
                groupId = int.Parse(readKeyItemGroupMenu.KeyChar.ToString());

                if (messageName == null)
                {
                    AddNewPlant(keyId, groupId, 0);
                }
                else
                {
                    //NameId
                    readKeyItemName = _actionService.LoadMenu(messageName, name, out outName);
                    nameId = int.Parse(readKeyItemName.KeyChar.ToString());
                    AddNewPlant(keyId, groupId, nameId);
                }
            }
            catch (Exception e)
            {
                _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();

            }
        }
        public void AddNewPlant(int keyTypeId, int keyGroupId, int keyNameId)
        {
            try
            {
                PlantMethodParameters plant = new PlantMethodParameters();

                //vegetable/friut/herb
                plant.IdType = keyTypeId;
                //nightshade/cuucurbits,pitted itd
                plant.GroupId = keyGroupId;
                //tomato,strawberry itd
                plant.NameId = keyNameId;

                PlantDetails(plant, null);
                Plant newPlant = new Plant(plant);
                _plantService.AddItem(newPlant);

                Console.WriteLine();
                Console.WriteLine("Plant added.");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();
            }
        }
        public void EditPlant(FileService fileService,ConsoleKeyInfo fileKey)
        {
            try
            {
                Console.WriteLine();
                Console.Write("Enter Id or name of plant what you want to edit:");
                string value = Console.ReadLine();
                Console.WriteLine();
                int id;

                if (int.TryParse(value, out id) == true)
                {
                    var find = _plantService.GetItemById(id);

                    if (find != null)
                    {
                        var newPlant = EditItem(find);
                        _plantService.UpdateItem(newPlant);
                        if(fileKey.KeyChar == '1')
                            fileService.EditPlantInXMLFile(newPlant);
                        else if (fileKey.KeyChar == '2')
                            fileService.FileErrorsJSONWriter();

                        Console.WriteLine();
                        Console.WriteLine("Plant edited.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(message._NO_RESULT_FIND);
                    }
                }
                else
                {
                    var find = _plantService.GetItemByName(value);

                    if (find != null)
                    {
                        var newPlant = EditItem(find);
                        _plantService.UpdateItem(newPlant);
                        if (fileKey.KeyChar == '1')
                            fileService.EditPlantInXMLFile(newPlant);
                        else if (fileKey.KeyChar == '2')
                            fileService.FileErrorsJSONWriter();

                        Console.WriteLine();
                        Console.WriteLine("Plant edited.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(message._NO_RESULT_FIND);
                    }

                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
               _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();
            }
        }
        public Plant EditItem(Plant item)
        {
            PlantMethodParameters plant = new PlantMethodParameters(item);

            _actionService.EditMenu();

            string choosenFields = Console.ReadLine();
            try
            {
                if (choosenFields == "*")
                    PlantDetails(plant, null);
                else
                {
                    bool match = data.PlantEditValidation(choosenFields);

                    if (match == true)
                    {
                        string[] splitedStringArray = choosenFields.Split(",");
                        var splitedStringArrayAfterValidation = data.DeleteTheSameChoosenFields(splitedStringArray);

                        PlantDetails(plant, splitedStringArrayAfterValidation);

                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(message._ERROR);
                    }
                }
            }
            catch (Exception e)
            {
               _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();
            }
            Plant editedPlant = new Plant(plant);

            return editedPlant;
        }
        public void RemovePlant(FileService fileService, ConsoleKeyInfo fileKey)
        {
            Console.WriteLine();
            Console.Write("Enter Id or name of plant what you want to remove:");
            string value = Console.ReadLine();
            Console.WriteLine();
            int id;

            try
            {
                if (int.TryParse(value, out id) == true)
                {
                    var find = _plantService.GetItemById(id);

                    if (find == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine(message._NO_RESULT_FIND);
                    }
                    else
                    {
                        _plantService.RemoveItem(find);
                        Console.WriteLine();
                        Console.WriteLine("Plant removed");
                        if (fileKey.KeyChar == '1')
                            fileService.DeleteItemFromXMLFile(value,1);
                        else if (fileKey.KeyChar == '2')
                            fileService.FileErrorsJSONWriter();

                    }
                }
                else
                {
                    var find = _plantService.GetItemByName(value);
                    if (find == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine(message._NO_RESULT_FIND);
                    }
                    else
                    {
                        _plantService.RemoveItem(find);
                        Console.WriteLine();
                        Console.WriteLine("Plant removed");
                        if (fileKey.KeyChar == '1')
                            fileService.DeleteItemFromXMLFile(find.Id.ToString(),1);
                        else if (fileKey.KeyChar == '2')
                            fileService.FileErrorsJSONWriter();
                    }
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
               _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();
            }
        }
        public void SearchPlantByName()
        {
            Console.WriteLine();
            Console.Write("Enter name:");
            string name = Console.ReadLine();

            try
            {
                var find = _plantService.GetItemByName(name);

                if (find != null)
                {

                    Console.WriteLine();
                    Console.WriteLine($"Id:{find.Id}", Console.ForegroundColor = ConsoleColor.Red);
                    Console.WriteLine($"Name:{find.FullName}", Console.ForegroundColor = ConsoleColor.Blue);
                    Console.WriteLine($"Type:{find.Type}", Console.ForegroundColor = ConsoleColor.White);
                    Console.WriteLine($"Destination:{find.Destination}");
                    Console.WriteLine($"Color:{find.Color}");
                    Console.WriteLine($"Description:{find.Description}");
                    Console.WriteLine($"Opinion:{find.Opinion}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(message._NO_RESULTS);
                }
            }
            catch (Exception e)
            {
               _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();
            }
            Console.WriteLine();
        }
        public void ShowAllPlants(string messageWhat, string menuName, string messageItemName, ConsoleKeyInfo key)
        {
            try
            {
                if (key.KeyChar == '3')
                {
                    readKeyItemGroupMenu = _actionService.ShowMenu(messageWhat, menuName);
                    int id = int.Parse(readKeyItemGroupMenu.KeyChar.ToString());


                    var find = _plantService.GetAllItems(id, null);

                    if (find.Count != 0)
                    {
                        ShowAllFindedPlants(find);
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(message._EMPTY);
                    }
                }
                else
                {
                    readKeyItemGroupMenu = _actionService.LoadMenu(messageWhat, menuName, out name);
                    int id = int.Parse(readKeyItemGroupMenu.KeyChar.ToString());
                    readKeyItemName = _actionService.LoadMenu(messageItemName, name, out name);
                    int NameId = int.Parse(readKeyItemName.KeyChar.ToString());

                    var find = _plantService.GetAllItems(id, NameId);

                    if (find.Count != 0)
                    {
                        ShowAllFindedPlants(find);
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(message._EMPTY);
                    }
                }
            }
            catch (Exception e)
            {
               _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();
            }
            Console.WriteLine();
        }
        public void ShowAllFindedPlants(List<Plant> find)
        {
            try
            {
                for (int i = 0; i < find.Count; i++)
                {


                    //Console.WriteLine(find.ToStringTable(new[] { "Id", "Name","Type","Color","Destination","Description","Opinion" }, 
                    // a => a.Id, a => a.FullName,a=>a.Type,a=>a.Color,a=>a.Destination,a=>a.Description,a=>a.Opinion));
                    Console.WriteLine();
                    Console.WriteLine($"Id:{find[i].Id}", Console.ForegroundColor = ConsoleColor.Red);
                    Console.WriteLine($"Name:{find[i].FullName}", Console.ForegroundColor = ConsoleColor.Blue);
                    Console.WriteLine($"Type:{find[i].Type}", Console.ForegroundColor = ConsoleColor.White);
                    Console.WriteLine($"Color:{find[i].Color}");
                    Console.WriteLine($"Destination:{find[i].Destination}");
                    Console.WriteLine($"Description:{find[i].Description}");
                    Console.WriteLine($"Opinion:{find[i].Opinion}");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
               _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();
            }
            Console.WriteLine();

        }
        public void PlantDetails(PlantMethodParameters plant, string[] fields)
        {
            string type, destination;

            Console.WriteLine();
            try
            {
                if (fields == null)
                {
                    Console.Write("Name:");
                    plant.FullName = Console.ReadLine();

                    if (plant.IdType == 1)
                    {
                        if (plant.GroupId == 1)
                        {
                            if (plant.NameId == 1) //tomato
                            {
                                Console.Write(message.ReturnMessageById(plant.IdType, plant.NameId));
                                type = Console.ReadLine();
                                data.PlantDetailsValidation(type, null, plant);
                                //plant.Type = Console.ReadLine();
                            }
                            else
                            {
                                if (plant.NameId == 2) //pepper
                                {
                                    Console.Write(message.ReturnMessageById(plant.IdType, plant.NameId));
                                    type = Console.ReadLine();
                                    data.PlantDetailsValidation(type, null, plant);
                                    //plant.Type = Console.ReadLine();
                                }
                                else
                                {
                                    Console.Write("No specified type.");
                                    plant.Type = "No specified type.";
                                    Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            Console.Write("No specified type.");
                            plant.Type = "No specified type.";
                            Console.WriteLine();
                        }
                    }
                    if (plant.IdType == 2)
                    {
                        Console.Write(message.ReturnMessageById(plant.IdType, null));
                        type = Console.ReadLine();
                        data.PlantDetailsValidation(type, null, plant);
                        //plant.Type = Console.ReadLine();
                    }

                    if (plant.IdType == 3)
                    {
                        Console.Write(message.ReturnMessageById(plant.IdType, null));
                        type = Console.ReadLine();
                        data.PlantDetailsValidation(type, null, plant);
                        //plant.Type = Console.ReadLine();
                    }
                    Console.Write(message._PLANT_DESTINATION);
                    destination = Console.ReadLine();
                    data.PlantDetailsValidation(null, destination, plant);
                    Console.Write("Color:");
                    plant.Color = Console.ReadLine();
                    Console.Write("Description:");
                    plant.Description = Console.ReadLine();
                    Console.Write("Please share your opinion about this plant with another users:");
                    plant.Opinion = Console.ReadLine();

                }
                else
                {
                    for (int i = 0; i < fields.Length; i++)
                    {
                        if (fields[i] == "1")
                        {
                            Console.Write("Name:");
                            plant.FullName = Console.ReadLine();
                        }
                        if (fields[i] == "2")
                        {
                            if (plant.IdType == 1)
                            {
                                if (plant.GroupId == 1)
                                {
                                    if (plant.NameId == 1)
                                    {
                                        Console.Write(message.ReturnMessageById(plant.IdType, plant.NameId));
                                        data.PlantDetailsValidation(Console.ReadLine(), null, plant);
                                    }
                                    else
                                    {
                                        if (plant.NameId == 2)
                                        {
                                            Console.Write(message.ReturnMessageById(plant.IdType, plant.NameId));
                                            data.PlantDetailsValidation(Console.ReadLine(), null, plant);
                                        }
                                        else
                                        {
                                            Console.Write("No specified type.");
                                            plant.Type = "No specified type.";
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Write("No specified type.");
                                    plant.Type = "No specified type.";
                                }
                            }
                            if (plant.IdType == 2)
                            {
                                Console.Write(message.ReturnMessageById(plant.IdType, null));
                                data.PlantDetailsValidation(Console.ReadLine(), null, plant);
                            }

                            if (plant.IdType == 3)
                            {
                                Console.Write(message.ReturnMessageById(plant.IdType, null));
                                data.PlantDetailsValidation(Console.ReadLine(), null, plant);
                            }
                        }
                        if (fields[i] == "3")
                        {
                            Console.Write(message._PLANT_DESTINATION);
                            data.PlantDetailsValidation(null, Console.ReadLine(), plant);
                        }
                        if (fields[i] == "4")
                        {
                            Console.Write("Color:");
                            plant.Color = Console.ReadLine();
                        }
                        if (fields[i] == "5")
                        {
                            Console.Write("Description:");
                            plant.Description = Console.ReadLine();
                        }
                        if (fields[i] == "6")
                        {
                            Console.Write("Please share your opinion about this plant with another users:");
                            plant.Opinion = Console.ReadLine();
                        }
                    }
                }
            }
            catch (Exception e)
            {
               _errorService.CatchError(e, _errorService);
                //if (fileKey.KeyChar == '1')
                //    _fileService.FileErrorsXMLWriter();
                //else
                //    _fileService.FileErrorsJSONWriter();
            }
        }
    }
}


