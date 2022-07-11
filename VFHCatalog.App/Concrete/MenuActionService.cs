using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App.Common;
using VFHCatalog.Domain.Entity;
using VFHCatalog.App.Helpers;

namespace VFHCatalog.App.Concrete
{
    public class MenuActionService: BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }

        MessagesService message = new MessagesService();
       
        public List<MenuAction> GetMenuActionByName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();
            foreach (var menuAction in Items)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }

            return result;

        }
        public ConsoleKeyInfo AddNewMenuView( string menuName, out string name)
        {
            var addNewItemMenu = GetMenuActionByName(menuName);
            for (int i = 0; i < addNewItemMenu.Count; i++)
            {
                Console.WriteLine($"{addNewItemMenu[i].Id}.{addNewItemMenu[i].Name}");
            }

            var readKey = Console.ReadKey();

            var id = addNewItemMenu.Find(e => e.Id == int.Parse(readKey.KeyChar.ToString()));

            if (id == null)
            {
                Console.WriteLine();
                Console.WriteLine(message._ERROR);
                name = null;
            }
            else
                name = id.Name;

            return readKey;
        }
        public ConsoleKeyInfo LoadMenu(string message, string menuName, out string name)
        {
            Console.WriteLine(message);
            Console.WriteLine();

            var mainMenu = GetMenuActionByName(menuName);
            for (int i = 0; i < mainMenu.Count; i++)
            {
                Console.WriteLine($"{mainMenu[i].Id}.{mainMenu[i].Name}");
            }

            var readKey = Console.ReadKey();
            Console.WriteLine();

            var id = mainMenu.Find(e => e.Id == int.Parse(readKey.KeyChar.ToString()));
            name = id.Name;

            return readKey;
        }
        public ConsoleKeyInfo ShowMenu(string message, string menuName)
        {
            Console.WriteLine(message);
            Console.WriteLine();
            var mainMenu = GetMenuActionByName(menuName);
            for (int i = 0; i < mainMenu.Count; i++)
            {
                Console.WriteLine($"{mainMenu[i].Id}.{mainMenu[i].Name}");
            }

            var readKey = Console.ReadKey();
            Console.WriteLine();

            return readKey;

        }
        public void EditMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What fields do you want to change?Please separe numbers of your choosen fields by ',':");
            Console.WriteLine("1-Name");
            Console.WriteLine("2-Type");
            Console.WriteLine("3-Destination");
            Console.WriteLine("4-Color");
            Console.WriteLine("5-Description");
            Console.WriteLine("6-Opinion");
            Console.WriteLine("*-Edit all fields");
        }
        private void Initialize()
        {

            AddMenuItem(new MenuAction(1, "Show all errors", "Errors"));
            AddMenuItem(new MenuAction(2, "Show logs from today","Errors"));
            AddMenuItem(new MenuAction(3, "Delete the log that has been resolved", "Errors"));
            AddMenuItem(new MenuAction(4, "Back to start menu.", "Errors"));
            
            AddMenuItem(new MenuAction(1, "Add", "Main"));
            AddMenuItem(new MenuAction(2, "Edit", "Main"));
            AddMenuItem(new MenuAction(3, "Remove", "Main"));
            AddMenuItem(new MenuAction(4, "Search by name", "Main"));
            AddMenuItem(new MenuAction(5, "View all", "Main"));
            AddMenuItem(new MenuAction(6, "Back to start menu.", "Main"));

            AddMenuItem(new MenuAction(1, "Vegetable", "PlantMenu"));
            AddMenuItem(new MenuAction(2, "Fruit", "PlantMenu"));
            AddMenuItem(new MenuAction(3, "Herb", "PlantMenu"));

            AddMenuItem(new MenuAction(1, "Nightshade", "Vegetable"));
            AddMenuItem(new MenuAction(2, "Cucurbits", "Vegetable"));
            AddMenuItem(new MenuAction(3, "Legumes", "Vegetable"));
            AddMenuItem(new MenuAction(4, "Cruciferous", "Vegetable"));
            AddMenuItem(new MenuAction(5, "Leafy", "Vegetable"));
            AddMenuItem(new MenuAction(6, "Onion", "Vegetable"));
            AddMenuItem(new MenuAction(7, "Root", "Vegetable"));
            AddMenuItem(new MenuAction(8, "Turnip greens", "Vegetable"));


            AddMenuItem(new MenuAction(1, "Tomato", "Nightshade"));
            AddMenuItem(new MenuAction(2, "Pepper", "Nightshade"));
            AddMenuItem(new MenuAction(3, "Potato", "Nightshade"));
            AddMenuItem(new MenuAction(4, "Eggplant", "Nightshade"));
            AddMenuItem(new MenuAction(5, "Other", "Nightshade"));

            AddMenuItem(new MenuAction(1, "Cucumber", "Cucurbits"));
            AddMenuItem(new MenuAction(2, "Zucchini", "Cucurbits"));
            AddMenuItem(new MenuAction(3, "Pumpkin", "Cucurbits"));
            AddMenuItem(new MenuAction(4, "Patison", "Cucurbits"));
            AddMenuItem(new MenuAction(5, "Other", "Cucurbits"));

            AddMenuItem(new MenuAction(1, "Beans", "Legumes"));
            AddMenuItem(new MenuAction(2, "Pea", "Legumes"));
            AddMenuItem(new MenuAction(3, "Lentils", "Legumes"));
            AddMenuItem(new MenuAction(4, "Broad bean", "Legumes"));
            AddMenuItem(new MenuAction(5, "Other", "Legumes"));

            AddMenuItem(new MenuAction(1, "Cabbage", "Cruciferous"));
            AddMenuItem(new MenuAction(2, "Brussels sprouts", "Cruciferous"));
            AddMenuItem(new MenuAction(3, "Broccoli", "Cruciferous"));
            AddMenuItem(new MenuAction(4, "Cauliflower", "Cruciferous"));
            AddMenuItem(new MenuAction(5, "Kohlrabi", "Cruciferous"));
            AddMenuItem(new MenuAction(6, "Other", "Cruciferous"));

            AddMenuItem(new MenuAction(1, "Lettuce", "Leafy"));
            AddMenuItem(new MenuAction(2, "Spinach", "Leafy"));
            AddMenuItem(new MenuAction(3, "Leaf parsley", "Leafy"));
            AddMenuItem(new MenuAction(4, "Other", "Leafy"));

            AddMenuItem(new MenuAction(1, "Onion", "Onion"));
            AddMenuItem(new MenuAction(2, "Garlic", "Onion"));
            AddMenuItem(new MenuAction(3, "Leek", "Onion"));
            AddMenuItem(new MenuAction(4, "Other", "Onion"));

            AddMenuItem(new MenuAction(1, "Carrot", "Root"));
            AddMenuItem(new MenuAction(2, "Root parsley", "Root"));
            AddMenuItem(new MenuAction(3, "Beetroot", "Root"));
            AddMenuItem(new MenuAction(4, "Root celery", "Root"));
            AddMenuItem(new MenuAction(5, "Other", "Root"));

            AddMenuItem(new MenuAction(1, "Radish", "Turnip greens"));
            AddMenuItem(new MenuAction(2, "Rutabaga", "Turnip greens"));
            AddMenuItem(new MenuAction(3, "Turnip", "Turnip greens"));
            AddMenuItem(new MenuAction(4, "Other", "Turnip greens"));

            AddMenuItem(new MenuAction(1, "Pitted", "Fruit"));
            AddMenuItem(new MenuAction(2, "Berry", "Fruit"));
            AddMenuItem(new MenuAction(3, "Pome", "Fruit"));
            AddMenuItem(new MenuAction(4, "Citrus", "Fruit"));
            AddMenuItem(new MenuAction(5, "Exotic", "Fruit"));

            AddMenuItem(new MenuAction(1, "Cherries", "Pitted"));
            AddMenuItem(new MenuAction(2, "Peach", "Pitted"));
            AddMenuItem(new MenuAction(3, "Plum", "Pitted"));
            AddMenuItem(new MenuAction(4, "Apricot", "Pitted"));
            AddMenuItem(new MenuAction(5, "Other", "Pitted"));

            AddMenuItem(new MenuAction(1, "Strawberry", "Berry"));
            AddMenuItem(new MenuAction(2, "Blackberries", "Berry"));
            AddMenuItem(new MenuAction(3, "Blueberries", "Berry"));
            AddMenuItem(new MenuAction(4, "Raspberries", "Berry"));
            AddMenuItem(new MenuAction(5, "Currants", "Berry"));
            AddMenuItem(new MenuAction(6, "Berries", "Berry"));
            AddMenuItem(new MenuAction(7, "Other", "Berry"));

            AddMenuItem(new MenuAction(1, "Apple", "Pome"));
            AddMenuItem(new MenuAction(2, "Pear", "Pome"));
            AddMenuItem(new MenuAction(3, "Quince", "Pome"));
            AddMenuItem(new MenuAction(4, "Pomegranate ", "Pome"));
            AddMenuItem(new MenuAction(5, "Other", "Pome"));

            AddMenuItem(new MenuAction(1, "Lemon", "Citrus"));
            AddMenuItem(new MenuAction(2, "Tangerine", "Citrus"));
            AddMenuItem(new MenuAction(3, "Orange", "Citrus"));
            AddMenuItem(new MenuAction(4, "Grapefruit", "Citrus"));
            AddMenuItem(new MenuAction(5, "Other", "Citrus"));

            AddMenuItem(new MenuAction(1, "Banana", "Exotic"));
            AddMenuItem(new MenuAction(2, "Pineapple", "Exotic"));
            AddMenuItem(new MenuAction(3, "Lychee", "Exotic"));
            AddMenuItem(new MenuAction(4, "Other", "Exotic"));

            AddMenuItem(new MenuAction(1, "Healing", "Herb"));
            AddMenuItem(new MenuAction(2, "Spicy", "Herb"));
            AddMenuItem(new MenuAction(3, "Essential oil", "Herb"));

        }
    }
}
