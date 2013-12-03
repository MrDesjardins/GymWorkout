using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;

namespace WorkoutPlanner.Tests.Models
{
    [TestClass]
    public class WorkoutSessionTest
    {
        private WorkoutSession _entity;
        private Mock<WorkoutSession> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _entity = new WorkoutSession();
            _mock = new Mock<WorkoutSession>(MockBehavior.Strict);
        }

        [TestMethod]
        public void WorkoutSessionTestWorkoutSessionWithNoName_Validate_Invalid()
        {
            //Arrange
            _entity = new WorkoutSession { Name = string.Empty, Workout = new Workout()};

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }


        [TestMethod]
        public void WorkoutSessionTestWorkoutSessionWithNullName_Validate_Invalid()
        {
            //Arrange
            _entity = new WorkoutSession { Name = null, Workout = new Workout() };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }

        [TestMethod]
        public void WorkoutSessionTestWorkoutSessionWithNullWorkout_Validate_Invalid()
        {
            //Arrange
            _entity = new WorkoutSession { Name = "Name", Workout = null };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }

        [TestMethod]
        public void WorkoutSessionTestWorkoutSessionWithExerciceWithSameOrder_Validate_Invalid()
        {
            //Arrange
            _entity = new WorkoutSession {Name = "Name", Workout = new Workout(), WorkoutSessionExercises = new List<WorkoutSessionExercise> {new WorkoutSessionExercise {Order = 1}, new WorkoutSessionExercise {Order = 1}}};
            
            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 1);
        }


        [TestMethod]
        public void WorkoutSessionTestWorkoutSessionWithExerciceWithDiffOrder_Validate_Valid()
        {
            //Arrange
            _entity = new WorkoutSession { Name = "Name", Workout = new Workout(), WorkoutSessionExercises = new List<WorkoutSessionExercise> { new WorkoutSessionExercise { Order = 1 }, new WorkoutSessionExercise { Order = 2 } } };

            //Act
            var errorsList = new List<ValidationResult>(_entity.Validate(new ValidationContext(this)));

            //Assert
            Assert.IsTrue(errorsList.Count == 0);
        }
    }
}