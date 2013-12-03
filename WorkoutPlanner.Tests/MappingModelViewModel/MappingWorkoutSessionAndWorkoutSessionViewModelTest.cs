using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Mappers;
using Mappers.Base;
using Mappers.Factory;
using Mappers.Implementations;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Setup.Ioc;
using ViewModels;
using WorkoutPlanner.Ioc;
using System.Linq;
namespace WorkoutPlanner.Tests.MappingModelViewModel
{
    [TestClass]
    public class MappingWorkoutSessionAndWorkoutSessionViewModelTest
    {
        private WorkoutSession _model;
        private WorkoutSessionViewModel _viewModel;
        private WorkoutSessionMapper _mapper;

        /// <summary>
        /// Register every mapping
        /// </summary>
        public MappingWorkoutSessionAndWorkoutSessionViewModelTest()
        {
            UnityConfiguration.Initialize();
            MapperConfiguration.Initialize(UnityConfiguration.Container.Resolve<IMapperFactory>());
        }

        [TestInitialize]
        public void Initialize()
        {
            _model = new WorkoutSession();
            _mapper = new WorkoutSessionMapper();
        }

        [TestMethod]
        public void WorkoutSessionMapping_ModelToViewModelDefaultValue_Valid()
        {
            //Arrange
            _model = new WorkoutSession();

            //Act
            _viewModel = _mapper.GetViewModel(_model);

            //Assert
            Assert.AreEqual(_model.Id,_viewModel.Id);
            Assert.AreEqual(_model.Name,_viewModel.Name);
            Assert.AreEqual(default(int),_viewModel.WorkoutId);
            Assert.AreEqual(default(int), _viewModel.Exercises.Count());
        }

        [TestMethod]
        public void WorkoutSessionMapping_ModelToViewModelAllFieldsWithValues_Valid()
        {
            //Arrange
            _model = new WorkoutSession();
            _model.Id = 1;
            _model.Name = "Test Workout Session";
            _model.WorkoutSessionExercises = new WorkoutSessionExercise[2]{new WorkoutSessionExercise(), new WorkoutSessionExercise()};
            _model.Workout = new Workout { Id = 1 };

            //Act
            _viewModel = _mapper.GetViewModel(_model);

            //Assert
            Assert.AreEqual(_model.Id, _viewModel.Id);
            Assert.AreEqual(_model.Name, _viewModel.Name);
            Assert.AreEqual(1, _viewModel.WorkoutId);
            Assert.AreEqual(_model.WorkoutSessionExercises.Count(), _viewModel.Exercises.Count());
        }

    }
}
