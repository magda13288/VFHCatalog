using System;
using VFHCatalog.App.Concrete;
using VFHCatalog.App.Managers;
using VFHCatalog.App.Helpers;
using VFHCatalog.Domain.Common;
using VFHCatalog.Domain.Entity;
using System.IO;

namespace VFHCatalog
{
    public class VFHCatalog
    {
        public void Run()
        {
            PlantService vegetablesService = new PlantService();
            PlantService fruitsService = new PlantService();
            PlantService herbsService = new PlantService();
            PlantService newVeges = new PlantService();
            PlantService newFruits = new PlantService();
            PlantService newHerbs = new PlantService();

            PlantManager vegetablesManager = new PlantManager();
            PlantManager fruitsManager = new PlantManager();
            PlantManager herbsManager = new PlantManager();

            FileService vegetableFile = new FileService();
            FileService fruitsFile = new FileService();
            FileService herbsFile = new FileService();
            FileService errorFileService = new FileService();

            MessagesService message = new MessagesService();
            MenuActionService actionService = new MenuActionService();

            ErrorsService errorsService = new ErrorsService();
            ErrorsService newErrorService = new ErrorsService();


            ConsoleKeyInfo keyInfoPlantMain, readKey, choosenFile;
            string name;

            int vegesLastId = default,fruitsLastId = default,herbsLastId = default, vegesServiceStartCount =default, fruitsServiceStartCount =default, herbsServiceStartCount = default, errorLastId = default; 


            Console.WriteLine();
            Console.WriteLine("Choose from what file load data: 1-XML,2-JSON");

            do
            {
                choosenFile = Console.ReadKey();
                Console.WriteLine();

                switch (choosenFile.KeyChar)
                {
                    case '1':

                        string vegesXMLPath = Directory.GetCurrentDirectory() + "\\FileService\\Vegetables.xml";
                        string fruitsXMLPath = Directory.GetCurrentDirectory() + "\\FileService\\Fruits.xml";
                        string herbsXMLPath = Directory.GetCurrentDirectory() + "\\FileService\\Herbs.xml";
                        string errorXMLPath = Directory.GetCurrentDirectory() + "\\FileService\\Errors.xml";

                        vegetableFile = new FileService(vegetablesService, vegesXMLPath, errorsService,errorXMLPath);
                        vegetableFile.FileErrorXMLReader();
                        errorLastId = errorsService.GetLastId();
                        vegetableFile.FileXMLReader();
                        vegesLastId = vegetablesService.GetLastId();
                        vegesServiceStartCount = vegetablesService.Items.Count;
                        vegetablesManager = new PlantManager(actionService, vegetablesService, errorsService, errorXMLPath,vegetableFile);

                        fruitsFile = new FileService(fruitsService, fruitsXMLPath, errorsService, errorXMLPath);
                        fruitsFile.FileXMLReader();
                        fruitsLastId = fruitsService.GetLastId();
                        fruitsServiceStartCount = fruitsService.Items.Count;
                        fruitsManager = new PlantManager(actionService, fruitsService,errorsService, errorXMLPath,fruitsFile);

                        herbsFile = new FileService(herbsService, herbsXMLPath, errorsService, errorXMLPath);
                        herbsFile.FileXMLReader();
                        herbsLastId = herbsService.GetLastId();
                        herbsServiceStartCount = herbsService.Items.Count;
                        herbsManager = new PlantManager(actionService, herbsService, errorsService, errorXMLPath,herbsFile);
                        Console.Clear();
                        break;

                    case '2':

                        string vegesJSONPath = Directory.GetCurrentDirectory() + "\\FileService\\Vegetables.txt";
                        string fruitsJSONPath = Directory.GetCurrentDirectory() + "\\FileService\\Fruits.txt";
                        string herbsJSONPath = Directory.GetCurrentDirectory() + "\\FileService\\Herbs.txt";
                        string errorJSONPath = Directory.GetCurrentDirectory() + "\\FileService\\Errors.txt";

                       
                        vegetableFile = new FileService(vegetablesService, vegesJSONPath, errorsService, errorJSONPath);
                        vegetableFile.FileErrorJSONReader();
                        errorLastId = errorsService.GetLastId();
                        vegetableFile.FileJSONReader();                   
                        vegesLastId = vegetablesService.GetLastId();
                        vegetablesManager = new PlantManager(actionService, vegetablesService, errorsService, errorJSONPath,vegetableFile);

                        fruitsFile = new FileService(fruitsService, fruitsJSONPath, errorsService, errorJSONPath);
                        fruitsFile.FileErrorJSONReader();
                        fruitsFile.FileJSONReader();                      
                        fruitsLastId = fruitsService.GetLastId();
                        fruitsManager = new PlantManager(actionService, fruitsService, errorsService, errorJSONPath,fruitsFile);

                        herbsFile = new FileService(herbsService, herbsJSONPath, errorsService, errorJSONPath);
                        herbsFile.FileErrorJSONReader();
                        herbsFile.FileJSONReader();                        
                        herbsLastId = herbsService.GetLastId();
                        herbsManager = new PlantManager(actionService, herbsService, errorsService, errorJSONPath,herbsFile);
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine(message._ERROR);
                        Console.WriteLine();
                        break;
                }
            }
            while (choosenFile.KeyChar != '1' && choosenFile.KeyChar != '2');

            Console.WriteLine();
            Console.WriteLine(message._WELCOME_MESSAGE);
            Console.WriteLine();

            while (true)
            {
                readKey = actionService.ShowMenu(message._WHAT_DO, message._MAIN);

                switch (readKey.KeyChar)
                {
                    case '1':

                        Console.WriteLine();
                        Console.WriteLine(message._WHAT_ADD);
                        Console.WriteLine();
                        //IdType /Vege/Fruit/Herb
                        keyInfoPlantMain = actionService.AddNewMenuView(message._PLANT_MENU, out name);
                        Console.WriteLine();

                        switch (keyInfoPlantMain.KeyChar)
                        {
                            case '1':

                                vegetablesManager.AddPlant(name, keyInfoPlantMain.KeyChar.ToString(), message._VEGE_WHAT, message._VEGE_NAME);

                                break;
                            case '2':

                                fruitsManager.AddPlant(name, keyInfoPlantMain.KeyChar.ToString(), message._FRUIT_WHAT, message._FRUIT_NAME);

                                break;
                            case '3':

                                herbsManager.AddPlant(name, keyInfoPlantMain.KeyChar.ToString(), message._HERB_WHAT, null);
                                break;

                            default:
                                Console.WriteLine(message._ERROR);
                                Console.WriteLine();
                                break;
                        }

                        break;

                    case '2':

                        Console.WriteLine();
                        Console.WriteLine(message._WHAT_EDIT);
                        Console.WriteLine();

                        keyInfoPlantMain = actionService.AddNewMenuView(message._PLANT_MENU, out name);
                        Console.WriteLine();

                        switch (keyInfoPlantMain.KeyChar)
                        {
                            case '1':

                                vegetablesManager.EditPlant(vegetableFile, choosenFile);

                                break;
                            case '2':

                                fruitsManager.EditPlant(fruitsFile, choosenFile);

                                break;
                            case '3':

                                herbsManager.EditPlant(herbsFile, choosenFile);
                                break;
                            default:
                                Console.WriteLine(message._ERROR);
                                Console.WriteLine();
                                break;
                        }

                        break;

                    case '3':

                        Console.WriteLine();
                        Console.WriteLine(message._WHAT_REMOVE);
                        Console.WriteLine();

                        keyInfoPlantMain = actionService.AddNewMenuView(message._PLANT_MENU, out name);
                        Console.WriteLine();

                        switch (keyInfoPlantMain.KeyChar)
                        {
                            case '1':

                                vegetablesManager.RemovePlant(vegetableFile, choosenFile);

                                break;
                            case '2':

                                fruitsManager.RemovePlant(fruitsFile, choosenFile);

                                break;
                            case '3':

                                herbsManager.RemovePlant(herbsFile, choosenFile);
                                break;
                            default:
                                Console.WriteLine(message._ERROR);
                                Console.WriteLine();
                                break;
                        }

                        break;

                    case '4':

                        Console.WriteLine();
                        Console.WriteLine(message._WHAT_SEARCH);
                        Console.WriteLine();

                        keyInfoPlantMain = actionService.AddNewMenuView(message._PLANT_MENU, out name);
                        Console.WriteLine();

                        switch (keyInfoPlantMain.KeyChar)
                        {
                            case '1':

                                vegetablesManager.SearchPlantByName();

                                break;
                            case '2':

                                fruitsManager.SearchPlantByName();

                                break;
                            case '3':

                                herbsManager.SearchPlantByName();
                                break;
                            default:
                                Console.WriteLine(message._ERROR);
                                Console.WriteLine();
                                break;
                        }
                        break;

                    case '5':

                        Console.WriteLine();
                        Console.WriteLine(message._WHAT_SEARCH);
                        Console.WriteLine();

                        keyInfoPlantMain = actionService.AddNewMenuView(message._PLANT_MENU, out name);
                        Console.WriteLine();

                        switch (keyInfoPlantMain.KeyChar)
                        {
                            case '1':

                                vegetablesManager.ShowAllPlants(message._VEGE_SEARCH, name, message._VEGE_NAME_SEARCH, keyInfoPlantMain);

                                break;
                            case '2':

                                fruitsManager.ShowAllPlants(message._FRUIT_SEARCH, name, message._FRUIT_NAME_SEARCH, keyInfoPlantMain);
                                break;
                            case '3':

                                herbsManager.ShowAllPlants(message._HERB_SEARCH, name, null, keyInfoPlantMain);

                                break;
                            default:
                                Console.WriteLine(message._ERROR);
                                Console.WriteLine();
                                break;
                        }
                        break;

                    case '6':

                        newVeges = vegetablesService.GetPlantFromID(vegesLastId, vegetablesService);
                        newFruits = fruitsService.GetPlantFromID(fruitsLastId, fruitsService);
                        newHerbs = herbsService.GetPlantFromID(herbsLastId, herbsService);
                        newErrorService = errorsService.GetErrorsFromId(errorLastId, errorsService);

                        switch (choosenFile.KeyChar)
                        {
                            case '1':

                                if (vegesServiceStartCount == 0)
                                    vegetableFile.FileXMLWriter(null);
                                else if (vegetablesService.Items.Count > vegesServiceStartCount)
                                    vegetableFile.FileXMLWriter(newVeges);

                                if (fruitsServiceStartCount == 0)
                                    fruitsFile.FileXMLWriter(null);
                                else if (fruitsService.Items.Count > fruitsServiceStartCount)
                                    fruitsFile.FileXMLWriter(newFruits);

                                if (herbsServiceStartCount == 0)
                                    herbsFile.FileXMLWriter(null);
                                else if (herbsService.Items.Count > herbsServiceStartCount)
                                    herbsFile.FileXMLWriter(newHerbs);

                                if (newErrorService.Items.Count > 0)
                                    vegetableFile.FileErrorsXMLWriter();

                                break;

                            case '2':

                                if (vegetablesService.Items.Count > 0)
                                    vegetableFile.FileJSONWriter();

                                if (fruitsService.Items.Count > 0)
                                    vegetableFile.FileJSONWriter();

                                if (herbsService.Items.Count > 0)
                                    vegetableFile.FileJSONWriter();

                                if (newErrorService.Items.Count > 0)
                                    vegetableFile.FileErrorsJSONWriter();

                                break;
                            default:
                                Console.WriteLine(message._ERROR);
                                Console.WriteLine();
                                break;
                        }
                        Program program = new Program();
                        program.MainMenu();
                        break;
                    default:
                        Console.WriteLine(message._ERROR);
                        Console.WriteLine();
                        break;
                }
            }
        }
    }

}
    

