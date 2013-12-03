using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Complex;
using Moq;

namespace WorkoutPlanner.Tests.Models
{
    [TestClass]
    public class ExerciseTest
    {
        private Exercise _entity;
        private Mock<Exercise> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _entity = new Exercise();
            _mock = new Mock<Exercise>(MockBehavior.Strict);
        }

        [TestMethod]
        public void ExerciceTestExerciseWithNoName_Validate_Invalid()
        {
            //Arrange
            _entity = new Exercise { Name = new LocalizedString { French = string.Empty, English = string.Empty },Muscle = new Muscle()};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 2);
        }

        [TestMethod]
        public void ExerciceTestExerciseWithNoMuscle_Validate_Invalid()
        {
            //Arrange
            _entity = new Exercise { Name = new LocalizedString { French = "French", English = "English" }, Muscle = null };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }

        [TestMethod]
        public void ExerciceTestExercise_Validate_Valid()
        {
            //Arrange
            _entity = new Exercise { Name = new LocalizedString { French = "French", English = "English" }, Muscle = new Muscle() };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 0);
        }

        [TestMethod]
        public void ExerciceTestExerciseWithNullName_Validate_Invalid()
        {
            //Arrange
            _entity = new Exercise { Name = null, Muscle = null };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }

        [TestMethod]
        public void ExerciceTestExerciseWithNullLocalizedName_Validate_Invalid()
        {
            //Arrange
            _entity = new Exercise { Name = new LocalizedString { French = null, English = null }, Muscle = new Muscle() };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 2);
        }
    }
}
