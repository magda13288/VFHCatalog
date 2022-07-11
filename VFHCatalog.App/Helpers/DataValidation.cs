using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VFHCatalog.Domain.Entity;
using System.Linq;

namespace VFHCatalog.App.Helpers
{
    public class DataValidation : Plant
    {
        MessagesService message = new MessagesService();

        public void PlantDetailsValidation(string dataType, string dataDestination, PlantMethodParameters plant)
        {
            bool match;
            Regex regex = RegexParam(plant);

                if (dataDestination == null)
                {
                     match = regex.IsMatch(dataType);

                do
                {
                    if (match == true)
                    {
                        plant.Type = dataType;
                    }
                    else
                    {
                        Console.WriteLine(message._ERROR);
                        if (plant.IdType == 1)
                        {
                            Console.Write(message.ReturnMessageById(plant.IdType, plant.NameId));
                            dataType = Console.ReadLine();
                        }
                        if (plant.IdType == 2 || plant.IdType == 3)
                        {
                            Console.Write(message.ReturnMessageById(plant.IdType, null));
                            dataType = Console.ReadLine();
                        }
                    }
                    match = regex.IsMatch(dataType);
                }
                while (match == false);
                }
                else
                {
                     regex = new Regex(@"\bground\/?|\bunder cowers\/?|\bpots\/?");
                     match = regex.IsMatch(dataDestination);
                do
                {
                    if (match == true)
                    {
                        plant.Destination = dataDestination;
                    }
                    else
                    {
                        Console.Write(message._PLANT_DESTINATION);
                        dataDestination = Console.ReadLine();
                        match = regex.IsMatch(dataDestination);
                    }
                }
                while (match == false);
            }
        }

        public Regex RegexParam(PlantMethodParameters plant)
        {
            Regex regex;

            if (plant.NameId != 0)
            {
                if (plant.IdType == 1)
                {
                    if (plant.GroupId == 1)
                    {
                        if (plant.NameId == 1)
                            regex = new Regex(@"\btall\/?|\bdwarf\/?|\bself-ending\/?|\bcoctail\/?");
                        else
                        {
                            if (plant.NameId == 2)
                                regex = new Regex(@"\bround\/?|\boblong\/?");
                            else
                                regex = null;
                        }
                    }
                    else
                        regex = null;
                }
                else
                {
                    if (plant.IdType == 2)
                        regex = new Regex(@"\bbush\/?|\btree\/?|\bclimbing\/?|\bhanging\/?");
                    else
                    
                        regex = null;           
                }
            }
            else
               {
                    if (plant.IdType == 3)
                        regex = new Regex(@"\bshrub\/?|\bvine\/?|\broot\/?");
                    else
                        regex = null;
                }
            
            return regex;
        }

        public bool PlantEditValidation(string field)
        {
            bool match;

            if (field.Length == 1)
            {
                Regex regex = new Regex("[1-6]");
                match = regex.IsMatch(field);

            }
            else
            {
                Regex regex = new Regex(@"^([1-6])(,[1-6]){1,6}$");
                match = regex.IsMatch(field);
            }

            return match;
        }

        public string[] DeleteTheSameChoosenFields(string[] splitedString)
        {
            List<string> stringList = splitedString.ToList();

            for (int i = 0; i < stringList.Count; i++)
            {
               var sameItems = stringList.FindAll(x => x == stringList[i]);

                if (sameItems.Count > 1)
                {
                    for (int j = 1; j < sameItems.Count; j++)
                    {
                        stringList.Remove(sameItems[j]);
                    }
                }
            }
            return stringList.ToArray();
        }
    }
}
