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
    public class MappingWorkoutAndWorkoutViewModelTest
    {
        private Workout _model;
        private WorkoutViewModel _viewModel;
        private WorkoutMapper _mapper;

        private Mock<Workout> _mock;

        /// <summary>
        /// Register every mapping
        /// </summary>
        public MappingWorkoutAndWorkoutViewModelTest()
        {
            UnityConfiguration.Initialize();
            MapperConfiguration.Initialize(UnityConfiguration.Container.Resolve<IMapperFactory>());
        }

        [TestInitialize]
        public void Initialize()
        {
            _model = new Workout();
            _mapper = new WorkoutMapper();
            _mock = new Mock<Workout>(MockBehavior.Strict);
        }

        [TestMethod]
        public void WorkoutMapping_ModelToViewModelDefaultValue_Valid()
        {
            //Arrange
            _model = new Workout();

            //Act
            _viewModel = _mapper.GetViewModel(_model);

            //Assert
            Assert.AreEqual(_model.Id,_viewModel.Id);
            Assert.AreEqual(_model.Name,_viewModel.Name);
            Assert.AreEqual(_model.Goal,_viewModel.Goal);
            Assert.AreEqual(_model.StartTime,_viewModel.StartTime);
            Assert.AreEqual(_model.EndTime,_viewModel.EndTime);
            Assert.AreEqual(0,_viewModel.Sessions.Count());
        }

        [TestMethod]
        public void WorkoutMapping_ModelToViewModelAllFieldsWithValues_Valid()
        {
            //Arrange
            _model = new Workout();
            _model.Id = 1;
            _model.Name = "Test Workout";
            _model.Goal = "Goal here";
            _model.StartTime = new DateTime(2013,01,28);
            _model.EndTime = _model.StartTime.AddDays(45);
            _model.Sessions = new Collection<WorkoutSession> {new WorkoutSession(), new WorkoutSession()};

            //Act
            _viewModel = _mapper.GetViewModel(_model);

            //Assert
            Assert.AreEqual(_model.Id, _viewModel.Id);
            Assert.AreEqual(_model.Name, _viewModel.Name);
            Assert.AreEqual(_model.Goal, _viewModel.Goal);
            Assert.AreEqual(_model.StartTime, _viewModel.StartTime);
            Assert.AreEqual(_model.EndTime, _viewModel.EndTime);
            Assert.AreEqual(_model.Sessions.Count(), _viewModel.Sessions.Count());
        }

    }
}
