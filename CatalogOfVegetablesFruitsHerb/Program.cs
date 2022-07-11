using System;
using VFHCatalog.App.Concrete;
using VFHCatalog.App.Managers;
using VFHCatalog.App.Helpers;
using VFHCatalog.Domain.Common;
using VFHCatalog.Domain.Entity;
using System.IO;

namespace VFHCatalog
{
    public class Program
    {
        static void Main(string[] args)
        {

            Program program = new Program();
            program.MainMenu();
            
        }

       public void MainMenu()
        {
            MessagesService message = new MessagesService();

            Console.WriteLine("Welcome. What you want to do?");
            Console.WriteLine();
            Console.WriteLine("1-> go to VFHCatalog");
            Console.WriteLine("2-> go to errors logs");
            Console.WriteLine("3-> Exit");

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':

                        Console.Clear();
                        VFHCatalog vFHCatalog = new VFHCatalog();
                        vFHCatalog.Run();
                        break;

                    case '2':
                        Console.Clear();
                        ErrorsLog log = new ErrorsLog();
                        log.Run();

                        break;
                    case '3':
                        Console.WriteLine("Bye bye and see you later.");
                        Console.WriteLine();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine(message._ERROR);
                        Console.WriteLine();
                        break;
                }
            }
            while (key.KeyChar != '1' && key.KeyChar != '2');
        }
    }
}
