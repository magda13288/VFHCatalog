using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.App.Common;
using VFHCatalog.Domain.Common;
using VFHCatalog.Domain.Entity;
using VFHCatalog.App.Helpers;
using System.Windows.Controls;
using System.Linq;


namespace VFHCatalog.App.Concrete
{
    public class PlantService : BasePlantService<Plant>
    {
        public PlantService GetPlantFromID(int lastId, PlantService _plantService)
        {
            PlantService plantService = new PlantService();
            var listOfPlants = _plantService.GetAllItemsFromID(lastId);
            if(listOfPlants.Count>0)
            plantService.AddItems(listOfPlants);
             
            
            return plantService;
             
        }
    } 
}

