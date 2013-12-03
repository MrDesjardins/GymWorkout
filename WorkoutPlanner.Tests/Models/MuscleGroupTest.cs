using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Complex;
using Moq;

namespace WorkoutPlanner.Tests.Models
{
    [TestClass]
    public class MuscleGroupTest
    {
        private MuscleGroup _entity;
        private Mock<MuscleGroup> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _entity = new MuscleGroup();
            _mock = new Mock<MuscleGroup>(MockBehavior.Strict);
        }

        [TestMethod]
        public void ExerciceTestMuscleGroupWithNoName_Validate_Invalid()
        {
            //Arrange
            _entity = new MuscleGroup { Name = new LocalizedString { French = string.Empty, English = string.Empty } };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 2);
        }

        [TestMethod]
        public void ExerciceTestMuscleGroupWithNoMuscle_Validate_Invalid()
        {
            //Arrange
            _entity = new MuscleGroup { Name = new LocalizedString { French = "French", English = "English" } };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 0);
        }

        [TestMethod]
        public void ExerciceTestMuscleGroup_Validate_Valid()
        {
            //Arrange
            _entity = new MuscleGroup { Name = new LocalizedString { French = "French", English = "English" } };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 0);
        }

        [TestMethod]
        public void ExerciceTestMuscleGroupWithNullName_Validate_Invalid()
        {
            //Arrange
            _entity = new MuscleGroup { Name = null};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }

        [TestMethod]
        public void ExerciceTestMuscleGroupWithNullLocalizedName_Validate_Invalid()
        {
            //Arrange
            _entity = new MuscleGroup { Name = new LocalizedString { French = null, English = null }};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 2);
        }
    }
}
