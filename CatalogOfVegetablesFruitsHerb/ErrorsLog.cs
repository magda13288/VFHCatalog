using System;
using System.Collections.Generic;
using VFHCatalog.App.Concrete;
using VFHCatalog.App.Managers;
using VFHCatalog.App.Helpers;
using VFHCatalog.Domain.Common;
using VFHCatalog.Domain.Entity;
using System.IO;
using System.Text;

namespace VFHCatalog
{
    public class ErrorsLog
    {
        public void Run()
        {

            MenuActionService actionService = new MenuActionService();
            MessagesService message = new MessagesService();
            ErrorsService errorsService = new ErrorsService();
            string errorXMLPath = Directory.GetCurrentDirectory() + "\\FileService\\Errors.xml";
            string errorJSONPath = Directory.GetCurrentDirectory() + "\\FileService\\Errors.txt";
            FileService fileService = new FileService();
            ErrorsLogManager errorManager = new ErrorsLogManager(); 
            ConsoleKeyInfo choosenFile; 

            Console.WriteLine();
            Console.WriteLine("Choose from what file load errors: 1-XML,2-JSON");

            do
            {
                choosenFile = Console.ReadKey();
                Console.WriteLine();

                switch (choosenFile.KeyChar)
                {
                    case '1':
                       fileService = new FileService(errorsService, errorXMLPath);
                       errorManager = new ErrorsLogManager(errorsService, errorXMLPath, fileService);
                       fileService.FileErrorXMLReader();
                        Console.Clear();
                        break;
                    case '2':
                        fileService = new FileService(errorsService, errorJSONPath);
                        errorManager = new ErrorsLogManager(errorsService, errorJSONPath, fileService);
                        fileService.FileErrorJSONReader();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine(message._ERROR);
                        Console.WriteLine();
                        break;
                }
            }
            while (choosenFile.KeyChar != '1' && choosenFile.KeyChar != '2');

           ConsoleKeyInfo readKey;

            while (true)
            {
                readKey = actionService.ShowMenu(message._WHAT_DO, message._ERRORS_MENU);
                Console.WriteLine();

                switch (readKey.KeyChar)
                {
                    case '1':

                        errorManager.ShowAllErrors();
                        break;
                    case '2':
                        errorManager.SelectErrorsFromToday();
                        break;
                    case '3':
                        errorManager.RemoveError(choosenFile);
                        break;
                    case '4':
                        Console.Clear();
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
