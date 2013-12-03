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
    public class MappingWorkoutSessionExerciseAndWorkoutSessionExerciseViewModelTest
    {
        private WorkoutSessionExercise _model;
        private WorkoutSessionExerciseViewModel _viewModel;
        private WorkoutSessionExerciseMapper _mapper;

        /// <summary>
        /// Register every mapping
        /// </summary>
        public MappingWorkoutSessionExerciseAndWorkoutSessionExerciseViewModelTest()
        {
            UnityConfiguration.Initialize();
            MapperConfiguration.Initialize(UnityConfiguration.Container.Resolve<IMapperFactory>());
        }

        [TestInitialize]
        public void Initialize()
        {
            _model = new WorkoutSessionExercise();
            _mapper = new WorkoutSessionExerciseMapper();
        }

        [TestMethod]
        public void WorkoutSessionExerciseMapping_ModelToViewModelDefaultValue_Valid()
        {
            //Arrange
            _model = new WorkoutSessionExercise();

            //Act
            _viewModel = _mapper.GetViewModel(_model);

            //Assert
            Assert.AreEqual(_model.Id,_viewModel.Id);
            Assert.AreEqual(_model.Order, _viewModel.Order);
            Assert.AreEqual(default(int), _viewModel.ExerciseId);
            Assert.AreEqual(default(string), _viewModel.ExerciseName);
            Assert.AreEqual(_model.Repetitions, _viewModel.Repetitions);
            Assert.AreEqual(_model.RestBetweenSetTicks, _viewModel.RestBetweenSetTicks);
            Assert.AreEqual(_model.Tempo, _viewModel.Tempo);
            Assert.AreEqual(_model.Weights, _viewModel.Weights);
            Assert.AreEqual(default(int), _viewModel.WorkoutSessionId);
            Assert.IsNull(_viewModel.ListExercise);
        }

        [TestMethod]
        public void WorkoutSessionExerciseMapping_ModelToViewModelAllFieldsWithValues_Valid()
        {
            //Arrange
            _model = new WorkoutSessionExercise();
            _model.Id = 100;
            _model.Order = 200;
            _model.Repetitions = "12-15";
            _model.RestBetweenExercices = new TimeSpan(0,0,0,30);
            _model.Tempo = "3-0-3";
            _model.Weights = "100-105";
            _model.WorkoutSession = new WorkoutSession { Id = 245};
            _model.Exercise = new Exercise { Id=922, Name = "Pushup"};
          

            //Act
            _viewModel = _mapper.GetViewModel(_model);

            //Assert
            Assert.AreEqual(_model.Id, _viewModel.Id);
            Assert.AreEqual(_model.Order, _viewModel.Order);
            Assert.AreEqual(_model.Exercise.Id, _viewModel.ExerciseId);
            Assert.AreEqual(_model.Exercise.Name.ToString(), _viewModel.ExerciseName);
            Assert.AreEqual(_model.Repetitions, _viewModel.Repetitions);
            Assert.AreEqual(_model.RestBetweenSetTicks, _viewModel.RestBetweenSetTicks);
            Assert.AreEqual(_model.Tempo, _viewModel.Tempo);
            Assert.AreEqual(_model.Weights, _viewModel.Weights);
            Assert.AreEqual(_model.WorkoutSession.Id, _viewModel.WorkoutSessionId);
            Assert.IsNull(_viewModel.ListExercise);
        }

    }
}
