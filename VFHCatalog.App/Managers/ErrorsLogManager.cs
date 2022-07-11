using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App.Concrete;
using VFHCatalog.App.Helpers;
using VFHCatalog.Domain.Entity;
using VFHCatalog.App.Common;
using System.IO;

namespace VFHCatalog.App.Managers
{
    public class ErrorsLogManager : Error
    {
        private ErrorsService _errorService;
        private readonly string _errorPath;
        private FileService _fileService;
        string path = Directory.GetCurrentDirectory();
        MessagesService message = new MessagesService();

        public ErrorsLogManager()
        { }
        public ErrorsLogManager(ErrorsService errorService, string errorPath, FileService fileService)
        {
            _errorService = errorService;
            _errorPath = errorPath;
            _fileService = fileService;
        }

        public void ShowAllErrors()
        {

            if (_errorService.Items.Count != 0)
            {
                for (int i = 0; i < _errorService.Items.Count; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Id:{_errorService.Items[i].Id}", Console.ForegroundColor = ConsoleColor.Red);
                    Console.WriteLine();
                    Console.WriteLine($"Message:{_errorService.Items[i].Message}", Console.ForegroundColor = ConsoleColor.White);
                    Console.WriteLine();
                    Console.WriteLine($"Source:{_errorService.Items[i].Source}");
                    Console.WriteLine();
                    Console.WriteLine($"StackTrace:{_errorService.Items[i].StackTrace}");
                    Console.WriteLine();
                    Console.WriteLine($"DateTime:{_errorService.Items[i].DateTime}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine(message._NO_ERRORS);
                Console.WriteLine();
            }
        }
        public void SelectErrorsFromToday()
        {
            var errors = _errorService.GetItems();

            if (errors.Count != 0)
            {
                for (int i = 0; i < errors.Count; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Id:{errors[i].Id}", Console.ForegroundColor = ConsoleColor.Red);
                    Console.WriteLine();
                    Console.WriteLine($"Message:{errors[i].Message}", Console.ForegroundColor = ConsoleColor.White);
                    Console.WriteLine();
                    Console.WriteLine($"Source:{errors[i].Source}");
                    Console.WriteLine();
                    Console.WriteLine($"StackTrace:{errors[i].StackTrace}");
                    Console.WriteLine();
                    Console.WriteLine($"DateTime:{errors[i].DateTime}");
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine(message._NO_ERRORS_FROM_TODAY);
                Console.WriteLine();
            }

        }

        public void RemoveError(ConsoleKeyInfo key)
        {
            Console.WriteLine();
            Console.Write("Enter id of error which you want to delete:");
            string value = Console.ReadLine();
            int id;

            try
            {
                if (int.TryParse(value, out id) == true)
                {
                    var find = _errorService.GetItemById(id);

                    if (find == null)
                    {
                        Console.WriteLine(message._NO_RESULT_FIND);
                        Console.WriteLine();
                    }
                    else
                    {
                        _errorService.RemoveItem(find);
                        if (key.KeyChar == '1')
                            _fileService.DeleteItemFromXMLFile(id.ToString(), 2);
                        else
                            _fileService.FileErrorsJSONWriter();

                        Console.WriteLine();
                        Console.WriteLine("Error removed.");
                        Console.WriteLine();

                    }
                }
            }
            catch(Exception e)
            {
                _errorService.CatchError(e, _errorService);
                if (key.KeyChar == '1')
                    _fileService.FileErrorsXMLWriter();
                else
                    _fileService.FileErrorsJSONWriter();
            }
            
            
            
        }
       
    }
}
