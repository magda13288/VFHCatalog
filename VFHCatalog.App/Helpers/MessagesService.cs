using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App.Common;
using VFHCatalog.Domain.Entity;

namespace VFHCatalog.App.Helpers
{
    public class MessagesService
    {
        string message;
        public string _ERROR = "Incorrect value";
        public string _WELCOME_MESSAGE = "Welcome in catalog of vegetables, fruits and herbs.";
        public string _WHAT_DO = "What you want to do?";
        public string _WHAT_ADD = "What you want to add?";
        public string _WHAT_SEARCH = "What you want to search?";
        public string _WHAT_REMOVE = "What you want to remove?";
        public string _WHAT_EDIT = "What you want to edit?";
        public string _MAIN = "Main";
        public string _PLANT_MENU = "PlantMenu";
        public string _ERRORS_MENU = "Errors";
        public string _PLANT_DESTINATION = "Destination(ground/under cowers/pots):";

        public string _VEGE_MENU = "Vegetable";
        public string _VEGE_WHAT = "From what group of the vegetables do you want to add? ";
        public string _VEGE_SEARCH = "From what group of the vegetables do you want to search?";
        public string _VEGE_NAME = "What vegetable do you want add?";
        public string _VEGE_NAME_SEARCH = "What vegetable do you want search?";
        public string _VEGE_EDIT = "Enter the name of vegetable which you want edit:";

        public string _FRUIT_MENU = "Fruit";
        public string _FRUIT_WHAT = "From what group of the fruits do you want to add? ";
        public string _FRUIT_SEARCH = "From what group of the fruits do you want to search? ";
        public string _FRUIT_NAME = "What fruit do you want add?";
        public string _FRUIT_NAME_SEARCH = "What fruit do you want search?";

        public string _HERB_MENU = "Herb";
        public string _HERB_WHAT = "From what group of the herbs do you want to add? ";
        public string _HERB_SEARCH = "From what group of the herbs do you want to search? ";

        public string _SEARCH = "What you want to search?";
        public string _NO_RESULTS = "The plant with the given name dosn' exist in the catalog. Or you enetered wrong name.";
        public string _EMPTY = "There are no results in the selected category.";
        public string _NO_RESULT_FIND = "The enetered plant dosn't exist in the list or you enetered incorrect value";
        public string _NO_ERRORS_FROM_TODAY = "There are no registered errors today.";
        public string _NO_ERRORS = "There are no registered errors.";

        public string _ERROR_SERVICE = "Unexpected error.";

        public string ReturnMessageById(int id,int? idName)
        {
            if (id == 1)
            {
                if (idName == 1)
                    message = "Type (tall/dwarf/self-ending/coctail):";
                else if(idName ==2)
                    message = "Type (round/oblong):";
            }
            if (id == 2)
                message = "Type (bush/tree/climbing/hanging):";
            if (id == 3)
                message = "Type (shrub/vine/root):";

            return message;
        }
    }
}
