using System;
using Xunit;
using FluentAssertions;
using Moq;
using VFHCatalog.App.Concrete;
using VFHCatalog.App.Common;
using VFHCatalog.App.Managers;
using VFHCatalog.App.Abstract;
using VFHCatalog.App.Helpers;
using VFHCatalog.Domain.Entity;


namespace VFHCatalog.Tests
{
    public class PlantServiceTests : Plant
    {

        [Fact]
        public void AddNewPlantAndCheckIfTheAdditionIsCorrect()
        {
            //Arrange

            var plantMethodParameters = SetPlantAddParameters(1);
            var plantService = new PlantService();
            var plant = new Plant(plantMethodParameters);

            //Act

            plantService.AddItem(plant);

            //Assert
            plant.Id.Should().Be(1);
            plantService.GetAllItems(1, null).Should().HaveCount(1);
            plantService.GetItemById(1).Should().BeSameAs(plant);

            plantService.RemoveItem(plant);
        }

        [Fact]
        public void EditPlantAndCheckIfTheEditionIsCorrect()
        {
            var plantMethodParameters = SetPlantAddParameters(1); //new plant
            var plantMethodParametersToEdit = SetPlantAddParameters(2); //to edit
            var plantService = new PlantService();
            var plant = new Plant(plantMethodParameters);
            var editedPlant = new Plant(plantMethodParametersToEdit);
            plantService.AddItem(plant);

            plantService.UpdateItem(editedPlant);
            var newEditedPlant = plantService.GetItemById(1);


            plantService.GetAllItems(1, null).Should().HaveCount(1);
            newEditedPlant.Id.Should().Be(1);
            newEditedPlant.FullName.Should().Be("Black Cherry Big");
            newEditedPlant.Type.Should().Be("tall");
            newEditedPlant.Destination.Should().Be("ground");

            plantService.RemoveItem(newEditedPlant);

        }

        [Fact]

        public void RemovePlantAndCheckThatPlantIsRemoved()
        {
            var plantMethodParameters = SetPlantAddParameters(1);
            var plantService = new PlantService();
            var plant = new Plant(plantMethodParameters);
            plantService.AddItem(plant);
            var find = plantService.GetItemById(1);

            plantService.RemoveItem(find);

            plantService.GetItemById(1).Should().BeNull();
        }

        [Fact]
        public void CheckTheCorrectnessOfTheSearchorThePlantByName()
        {
        
            var plantMethodParameters = SetPlantAddParameters(1);
            var secondPlantMethodParameters = SetPlantAddParameters(2);
            var plantService = new PlantService();
            var plant = new Plant(plantMethodParameters);
            var secondPlant = new Plant(secondPlantMethodParameters);

            plantService.AddItem(plant);
            plantService.AddItem(secondPlant);

            var find = plantService.GetItemByName("Black Cherry");

            find.Should().NotBeNull();
            find.Id.Should().Be(1);
            find.FullName.Should().Be("Black Cherry");
        }

        [Fact]
        public void ChecksTheCorrectnessOfSeearchingForAllPlantsFromAGivenGategoryAndGroup()
        {
            var plantMethodParameters = SetPlantAddParameters(1);
            var secondPlantMethodParameters = SetPlantAddParameters(2);
            var plantService = new PlantService();
            var plant = new Plant(plantMethodParameters);
            var secondPlant = new Plant(secondPlantMethodParameters);

            plantService.AddItem(plant);
            plantService.AddItem(secondPlant);

            var find = plantService.GetAllItems(1, 1);

            find.Should().NotBeNullOrEmpty();
            find.Should().HaveCount(2);
            find.Should().Contain(plant);
            find.Should().Contain(secondPlant);
            plant.Id.Should().Be(1);
            secondPlant.Id.Should().Be(2);

        }
        public PlantMethodParameters SetPlantAddParameters(int option)
        {
            var parameters = new PlantMethodParameters();

            if (option == 1)
            {
                parameters.IdType = 1;
                parameters.GroupId = 1;
                parameters.NameId = 1;
                parameters.FullName = "Black Cherry";
                parameters.Type = "tall/coctail";
                parameters.Destination = "ground";
                parameters.Color = "black";
                parameters.Description = "Description";
                parameters.Opinion = "Great, easy to growing, very tasty. I recommended";
            }
            if (option == 2)
            {
                parameters.Id = 1;
                parameters.IdType = 1;
                parameters.GroupId = 1;
                parameters.NameId = 1;
                parameters.FullName = "Black Cherry Big";
                parameters.Type = "tall";
                parameters.Destination = "ground";
                parameters.Color = "black";
                parameters.Description = "Description";
                parameters.Opinion = "Great, easy to growing, very tasty. I recommended";
            }
  

            return parameters;
        }


    }
}
