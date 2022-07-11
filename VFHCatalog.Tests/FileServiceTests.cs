using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Moq;
using VFHCatalog.App.Concrete;
using VFHCatalog.App.Common;
using VFHCatalog.App.Managers;
using VFHCatalog.App.Abstract;
using VFHCatalog.App.Helpers;
using VFHCatalog.Domain.Entity;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace VFHCatalog.Tests
{
    public class FileServiceTests:Plant
    {
        ErrorsService errorService = new ErrorsService();
        string _path = Directory.GetCurrentDirectory() + "\\FileServiceTests\\VegetablesTests.xml";
        string _errorPath = Directory.GetCurrentDirectory() + "\\FileServiceTests\\ErrorTest.xml";
        string path = Directory.GetCurrentDirectory() + "\\FileWriterErrors\\Errors.txt";
        PlantService pv = new PlantService();


        [Fact]
        public void CheckIfFileXMLWriterSavedFileCorrectly()
        {
            //Arrange

            var plantServiceToWrite = SetPlantServiceForTests();
            var plantServiceRead = new PlantService();

            var fileServiceWriter = new FileService(plantServiceToWrite, _path,errorService,_errorPath);
            var fileServiceReader = new FileService(plantServiceRead, _path, errorService, _errorPath);

            //Act
            fileServiceWriter.FileXMLWriter(plantServiceToWrite);
            fileServiceReader.FileXMLReader();
            File.Delete(_path);


            //Assert

            plantServiceToWrite.Should().NotBeNull();
            plantServiceRead.Should().NotBeNull();
            plantServiceRead.Items.Count.Should().Be(3);


          

        }

        [Fact]
        public void CheckIfXMLFileHasBeenCorrectlyEdited()
        {
            //Arrange

            var plantService = SetPlantServiceForTests();
            var plantServiceWriter = new FileService(plantService, _path, errorService, _errorPath);
            plantServiceWriter.FileXMLWriter(plantService);
            var plantServiceRead = new PlantService();
            var fileServiceReader = new FileService(plantServiceRead, _path, errorService, _errorPath);
            Plant plantEdit = new Plant() { Id = 1, GroupId = 1, IdType = 1, NameId = 1, FullName = "Black Cherry", Type = "tall/coctail", Color = "black", Destination = "ground/under cowers", Description = "ok", Opinion = "Very tasty and fertile tomato." };

            //Act

            fileServiceReader.EditPlantInXMLFile(plantEdit);
            fileServiceReader.FileXMLReader();
            var plantEdited = plantServiceRead.GetItemById(1);
            File.Delete(_path);

            //Assert

            plantEdited.Should().NotBeNull();
            plantEdited.Destination.Should().Be("ground/under cowers");

            

        }

        [Fact]
        public void ChechIfAnItemHasBeenRemovedFromTheXMLFile()
        {
            //Arrange

            var plantService = SetPlantServiceForTests();
            var fileService = new FileService(plantService, _path, errorService, _errorPath);
            fileService.FileXMLWriter(plantService);

            //Act
            fileService.DeleteItemFromXMLFile("2",1);

            XDocument xmlDoc = XDocument.Load(_path);

            var deletedPlant = xmlDoc.Descendants("Plant").Where(p => p.Attribute("Id").Value == "2").FirstOrDefault();
            File.Delete(_path);

            //Assert
            deletedPlant.Should().BeNull();

         
        }

        [Fact]

        public void CheckIfNewItemsAreAddedCorrectly()
        {
            //Arrange

            var plantService = SetPlantServiceForTests();
            var plantServiceWriter = new FileService(plantService, _path, errorService, _errorPath);
            plantServiceWriter.FileXMLWriter(plantService);
            var plantServiceRead = new PlantService();
            var fileServiceReader = new FileService(plantServiceRead, _path, errorService, _errorPath);

            var plantServiceToWrite = SetPlantsToAdd();
            var fileServiceWriter = new FileService(plantServiceToWrite, _path, errorService, _errorPath);


            //Act

            fileServiceWriter.FileXMLWriter(plantServiceToWrite);
            fileServiceReader.FileXMLReader();
            File.Delete(_path);

            //Assert

            plantServiceRead.Should().NotBeNull();
            plantServiceRead.Items.Count.Should().Be(6);

           
        }

        [Fact]
        public void CheckIfFileJSONWriterSavedFileCorrectly()
        {
            //Arrange

            var plantServiceToWrite = SetPlantServiceForTests();
            var plantServiceRead = new PlantService();
            var fileServiceWriter = new FileService(plantServiceToWrite, _path, errorService, _errorPath);
            var fileServiceReader = new FileService(plantServiceRead, _path, errorService, _errorPath);

            //Act

            fileServiceWriter.FileJSONWriter();
            fileServiceReader.FileJSONReader();
            File.Delete(_path);

            //Assert

            plantServiceToWrite.Should().NotBeNull();
            plantServiceRead.Should().NotBeNull();
            plantServiceRead.Items.Count.Should().Be(3);

           

        }

        [Fact]

        public void CheckIfErrorsFromFileXMLWriterAreSavedCorrectly()
        {
            //Arrange
           
            Error error = new Error() { Message = "error", Source = "line 289", StackTrace = "StackTrace", DateTime = DateTime.Now };
            errorService.AddItem(error);
            var fileService = new FileService(pv,_path, errorService, _errorPath);

            //Act
            fileService.FileErrorsXMLWriter();
            fileService.SaveErrorFromErrorWriter(errorService.Items[0], path);
            errorService.Items.Clear();

            var output = File.ReadAllText(path);
            fileService.FileErrorXMLReader();
            
          

            //Assert
            output.Should().NotBeNull();
            errorService.Items.Count.Should().Be(1);
            File.Exists(path).Should().BeTrue();

            File.Delete(_errorPath);
            File.Delete(path);

        }

        [Fact]
        public void CheckIfNewErrorWasAddedCorrectlyToXMLFile()
        {
            //Arrange

            Error error = new Error() { Message = "error", Source = "line 289", StackTrace = "StackTrace", DateTime = DateTime.Now };
            errorService.AddItem(error);
            var fileService = new FileService(pv, _path, errorService, _errorPath);
            fileService.FileErrorsXMLWriter();
            errorService.AddItem(error);

            //Act
            fileService.FileErrorsXMLWriter();
            errorService.Items.Clear();
            fileService.FileErrorXMLReader();
           

            //Assert

            File.Exists(path).Should().BeTrue();
            errorService.Items.Count.Should().Be(3);

            File.Delete(path);
            File.Delete(_errorPath);


        }

        [Fact]
        public void CheckIfFileXMLReaderErrorCatchException()
        {
            //Arrange
            string demagedFilePath = Directory.GetCurrentDirectory() + "\\FileServiceTests\\VegetablesDamaged.xml";
            var errorService = new ErrorsService();
            var plantService = new PlantService();

            var fileService = new FileService(plantService, demagedFilePath,errorService,_errorPath);
            fileService.FileXMLReader();
            fileService.FileErrorsXMLWriter();
            errorService.Items.Clear();
            //Act
            fileService.FileErrorXMLReader();
            
            //assert
            File.Exists(_errorPath).Should().BeTrue();
            errorService.Should().NotBeNull();

            File.Delete(_errorPath);

        }

        [Fact]
        public void CheckIfFileJSONReaderErrorCatchException()
        {
            //Arrange
            string demagedFilePath = Directory.GetCurrentDirectory() + "\\FileServiceTests\\VegetablesDamaged.txt";
            var errorService = new ErrorsService();
            var plantService = new PlantService();

            var fileService = new FileService(plantService, demagedFilePath, errorService, path);
            fileService.FileJSONReader();
            fileService.FileErrorsJSONWriter();
            errorService.Items.Clear();

            //Act
            fileService.FileErrorXMLReader();
            

            //Assert

            errorService.Should().NotBeNull();
            File.Delete(path);

        }

        public PlantService SetPlantServiceForTests()
        {
            var fisrtPlant = new Plant() { Id = 1, GroupId = 1, IdType = 1, NameId =1,FullName ="Black Cherry",Type="tall/coctail",Color="black",Destination="ground",Description="ok",Opinion="Very tasty and fertile tomato." };
            var secondPlant = new Plant() { Id = 2, GroupId = 1, IdType = 1, NameId = 1, FullName = "Green Zebra", Type = "tall", Color = "green", Destination = "ground/under cowers", Description = "ok", Opinion = "Very tasty and fertile tomato." };
            var thirdPlant = new Plant() { Id = 3, GroupId = 2, IdType = 1, NameId = 2, FullName = "Nefrettiti", Type = "No type sepcified.", Color = "green", Destination = "ground/pots", Description = "ok", Opinion = "ok." };

            var list = new List<Plant>();
            list.Add(fisrtPlant);
            list.Add(secondPlant);
            list.Add(thirdPlant);

            var plantService = new PlantService();
            plantService.AddItems(list);

            return plantService;
        }

        public PlantService SetPlantsToAdd()
        {

            var fisrtPlant = new Plant() { Id = 4, GroupId = 1, IdType = 1, NameId = 1, FullName = "GreenZebra", Type = "tall", Color = "green", Destination = "ground/under cowers", Description = "ok", Opinion = "Very tasty and fertile tomato." };
            var secondPlant = new Plant() { Id = 5, GroupId = 1, IdType = 1, NameId = 1, FullName = "Kiwi", Type = "tall", Color = "green", Destination = "ground/under cowers", Description = "ok", Opinion = "Very tasty and fertile tomato." };
            var thirdPlant = new Plant() { Id = 6, GroupId = 2, IdType = 1, NameId = 1, FullName = "Śremski", Type = "No type sepcified.", Color = "green", Destination = "ground/pots", Description = "ok", Opinion = "ok." };

            var list = new List<Plant>();
            list.Add(fisrtPlant);
            list.Add(secondPlant);
            list.Add(thirdPlant);

            var plantService = new PlantService();
            plantService.AddItems(list);

            return plantService;
        }


    }
}
