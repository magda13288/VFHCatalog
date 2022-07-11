using VFHCatalog.Domain.Common;
using System.Xml.Serialization;

namespace VFHCatalog.Domain.Entity
{
    public class Plant:BasePlantEntity
    {

        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("Color")]
        public string Color { get; set; }
        [XmlElement("Destination")]
        public string Destination { get; set; }
        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("Opinion")]
        public string Opinion { get; set; }

        public Plant()
        { }

        public Plant(PlantMethodParameters plantParameters)
        {
            Id = plantParameters.Id;
            IdType = plantParameters.IdType;
            GroupId = plantParameters.GroupId;
            NameId = plantParameters.NameId;
            FullName = plantParameters.FullName;
            Type = plantParameters.Type;
            Color = plantParameters.Color;
            Destination = plantParameters.Destination;
            Description = plantParameters.Description;
            Opinion = plantParameters.Opinion;
        }
        public class PlantMethodParameters:Plant
        {
            public PlantMethodParameters()
            { }
            public PlantMethodParameters(Plant plant)
            {
                Id = plant.Id;
                IdType = plant.IdType;
                GroupId = plant.GroupId;
                NameId = plant.NameId;
                FullName = plant.FullName;
                Type = plant.Type;
                Color = plant.Color;
                Destination = plant.Destination;
                Description = plant.Description;
                Opinion = plant.Opinion;
            }
        }        
    }
}
