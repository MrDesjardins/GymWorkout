using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mappers.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkoutPlanner.Tests.Others
{
    [TestClass]
    public class ModelViewModelMapperTest
    {
        [TestMethod]
        public void GetErrorPropertyMappedForTest()
        {
            //Arrange
            var map = new MapperTest();

            //Act
            var propertyNameInt = map.GetErrorPropertyMappedFor(d => d.PropertyInt);
            var propertyNameString = map.GetErrorPropertyMappedFor(d => d.PropertyString);
            var propertyNameObject = map.GetErrorPropertyMappedFor(d => d.PropertyObject.PropertyDeeperString);
            var propertyUnknown = map.GetErrorPropertyMappedFor(d => d.PropertyNotMapped);

            //Assert
            Assert.AreEqual("PropertyI", propertyNameInt);
            Assert.AreEqual("PropertyS", propertyNameString);
            Assert.AreEqual("PropertyDeepO", propertyNameObject);
            Assert.AreEqual("PropertyNotMapped", propertyUnknown);
        }

    }

    internal class MapperTest : ModelViewModelMapper<TestModel, TestViewModel>
    {

        public MapperTest()
        {
            base.AddModelViewModelToErrorsMap(d=>d.PropertyInt,e=>e.PropertyI);
            base.AddModelViewModelToErrorsMap(d=>d.PropertyString,e=>e.PropertyS);
            base.AddModelViewModelToErrorsMap(d=>d.PropertyObject.PropertyDeeperString,e=>e.PropertyDeepO);
        }

    }

    internal class TestModel
    {
        public string PropertyString { get; set; }
        public int PropertyInt { get; set; }
        public TestDeeperModel PropertyObject { get; set; }
        public string PropertyNotMapped { get; set; }
    }

    internal class TestDeeperModel
    {
        public string PropertyDeeperString { get; set; }

    }

    internal class TestViewModel
    {
        public string PropertyS { get; set; }
        public int PropertyI { get; set; }
        public string PropertyDeepO { get; set; }
    }
}
