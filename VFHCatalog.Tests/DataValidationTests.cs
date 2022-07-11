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


namespace VFHCatalog.Tests
{
    public class DataValidationTests : Plant
    {

        [Theory]

        [InlineData(1, 1, 1, @"\btall\/?|\bdwarf\/?|\bself-ending\/?|\bcoctail\/?")]
        [InlineData(1, 1, 2, @"\bround\/?|\boblong\/?")]
        [InlineData(2, 1, 1, @"\bbush\/?|\btree\/?|\bclimbing\/?|\bhanging\/?")]
        [InlineData(3, 1, 0, @"\bshrub\/?|\bvine\/?|\broot\/?")]

        public void CheckIfCorrectRegexIsReturned(int idType, int groupId, int nameId, string expectedRegex)
        {
            var plant = new PlantMethodParameters();
            var validation = new DataValidation();
            plant.IdType = idType;
            plant.GroupId = groupId;
            plant.NameId = nameId;

            var regex = validation.RegexParam(plant);

            Assert.Equal(expectedRegex, regex.ToString());

        }

        [Theory]

        [InlineData(1, 1, 3)]
        public void CheckIfReturnNullRegexForHerbs(int idType, int groupId, int nameId)
        {
            var plant = new PlantMethodParameters();
            var validation = new DataValidation();
            plant.IdType = idType;
            plant.GroupId = groupId;
            plant.NameId = nameId;

            var regex = validation.RegexParam(plant);
            
            regex.Should().BeNull();
        }

        [Theory]

        [InlineData("1,5,6")]
        [InlineData("1")]
        public void CheckIfRegexForFieldOfEditedPlantReturnTrue(string field)
        {
            var validation = new DataValidation();

            var value = validation.PlantEditValidation(field);

            value.Should().BeTrue();
        }

        [Theory]

        [InlineData("1,")]
        [InlineData("1/2/3")]
        [InlineData("7,8")]
        [InlineData("2-5")]
        [InlineData("1 2 3")]
        [InlineData("aaa")]
        [InlineData("a,a,a")]
        [InlineData("?,%,$")]
        [InlineData("1,2,3,4,5,6,6,6")]
        public void CheckIfRegexForFieldOfEditedPlantReturnFalse(string field)
        {
 
            var validation = new DataValidation();

            var value = validation.PlantEditValidation(field);

            value.Should().BeFalse();
        }

        [Theory]

        [InlineData(new string[] { "1", "1", "1" }, new string[] { "1" })]
        [InlineData(new string[] { "1", "2", "1","1" }, new string[] { "2","1" })]
        [InlineData(new string[] { "1", "2", "1", "2" }, new string[] { "1", "2" })]
        [InlineData(new string[] { "1", "2", "1", "2","3","4" }, new string[] { "1", "2","3","4" })]
        [InlineData(new string[] { "1", "2", "3", "1", "2", "3" }, new string[] { "1", "2", "3"})]
        [InlineData(new string[] { "6", "2", "3", "6", "4", "6" }, new string[] { "2", "3","4","6" })]
        public void CheckOccurenceOfFieldsForEditingPlant(string[] splitedStringArray, string[] expectedArray)
        {
            var data = new DataValidation();

            var validTable = data.DeleteTheSameChoosenFields(splitedStringArray);

            Assert.Equal(expectedArray, validTable);
        }
        
    }
}
