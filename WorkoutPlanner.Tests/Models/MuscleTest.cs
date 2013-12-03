using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Complex;
using Moq;

namespace WorkoutPlanner.Tests.Models
{
    [TestClass]
    public class MuscleTest
    {
        private Muscle _entity;
        private Mock<Muscle> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _entity = new Muscle();
            _mock = new Mock<Muscle>(MockBehavior.Strict);
        }

        [TestMethod]
        public void ExerciceTestMuscleWithNoName_Validate_Invalid()
        {
            //Arrange
            _entity = new Muscle { Name = new LocalizedString { French = string.Empty, English = string.Empty } };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 2);
        }

        [TestMethod]
        public void ExerciceTestMuscle_Validate_Valid()
        {
            //Arrange
            _entity = new Muscle { Name = new LocalizedString { French = "French", English = "English" } };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 0);
        }

        [TestMethod]
        public void ExerciceTestMuscleWithNullName_Validate_Invalid()
        {
            //Arrange
            _entity = new Muscle { Name = null };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }

        [TestMethod]
        public void ExerciceTestMuscleWithNullLocalizedName_Validate_Invalid()
        {
            //Arrange
            _entity = new Muscle { Name = new LocalizedString { French = null, English = null } };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }
    }
}
