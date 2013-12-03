using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;

namespace WorkoutPlanner.Tests.Models
{
    [TestClass]
    public class WorkoutTest
    {
        private Workout _entity;
        private Mock<Workout> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _entity = new Workout();
            _mock = new Mock<Workout>(MockBehavior.Strict);
        }

        [TestMethod]
        public void ExerciceTestWorkoutWithNoName_Validate_Invalid()
        {
            //Arrange
            _entity = new Workout { Name = string.Empty};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }

        [TestMethod]
        public void ExerciceTestWorkoutStartTimeBeforeEndTime_Validate_Valid()
        {
            //Arrange
            _entity = new Workout { Name = "Name", StartTime = DateTime.Today, EndTime = DateTime.Today.AddDays(1)};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 0);
        }


        [TestMethod]
        public void ExerciceTestWorkoutStartTimeWithNoEndTimeDefined_Validate_Valid()
        {
            //Arrange
            _entity = new Workout { Name = "Name", StartTime = DateTime.Today, EndTime = null};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 0);
        }


        [TestMethod]
        public void ExerciceTestWorkoutWithNullName_Validate_Invalid()
        {
            //Arrange
            _entity = new Workout { Name = null };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }


        [TestMethod]
        public void ExerciceTestWorkoutWithDateEndBeforeDateStart_Validate_Invalid()
        {
            //Arrange
            _entity = new Workout { Name = "Name",StartTime = DateTime.Today, EndTime = DateTime.Today.AddDays(-1)};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }


        [TestMethod]
        public void ExerciceTestWorkoutWithNoNameWithStartTime_Validate_Invalid()
        {
            //Arrange
            _entity = new Workout { Name = string.Empty, StartTime = DateTime.Today};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }

    }
}
