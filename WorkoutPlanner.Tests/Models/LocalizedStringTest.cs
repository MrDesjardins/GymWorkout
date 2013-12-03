using System;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Complex;

namespace WorkoutPlanner.Tests.Models
{
    [TestClass]
    public class LocalizedStringTest
    {
        [TestMethod]
        public void LocalizedStringAsStringTest()
        {
            //Arrange
            var loc = new LocalizedString();
            loc.French = "Test";
            loc.English = "Test";

            //Act
            string s = loc;

            //Assert
            Assert.AreEqual("Test",s);
        }

        [TestMethod]
        public void StringAsLocalizedStringTest()
        {
            //Arrange
            string s = "Test";
            var loc = new LocalizedString();

            //Act
            LocalizedString ls = s;

            //Assert
            Assert.AreEqual("Test",ls.Current);
        }

        [TestMethod]
        public void FrenchPropertyCurrentTest()
        {
            //Arrange
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("FR");
            
            //Act
            var loc = new LocalizedString { Current = "TestFrench" };

            //Assert
            Assert.AreEqual("TestFrench",loc.French);
            Assert.IsNull(loc.English);
        }


        [TestMethod]
        public void EnglishPropertyCurrentTest()
        {
            //Arrange
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("EN");

            //Act
            var loc = new LocalizedString { Current = "TestEnglish" };

            //Assert
            Assert.AreEqual("TestEnglish", loc.English);
            Assert.IsNull(loc.French);
        }
    }
}
