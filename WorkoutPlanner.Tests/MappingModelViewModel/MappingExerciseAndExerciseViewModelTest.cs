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
using Model.Complex;
using Moq;
using Setup.Ioc;
using ViewModels;
using WorkoutPlanner.Ioc;
using System.Linq;
namespace WorkoutPlanner.Tests.MappingModelViewModel
{
    [TestClass]
    public class MappingExerciseAndExerciseViewModelTest
    {
        private Exercise _model;
        private ExerciseViewModel _viewModel;
        private ExerciseMapper _mapper;

        /// <summary>
        /// Register every mapping
        /// </summary>
        public MappingExerciseAndExerciseViewModelTest()
        {
            UnityConfiguration.Initialize();
            var tt = UnityConfiguration.Container.Resolve<IMapperFactory>();
            MapperConfiguration.Initialize(tt);
        }



        [TestInitialize]
        public void Initialize()
        {
            _model = new Exercise();
            _mapper = new ExerciseMapper();
        }

        [TestMethod]
        public void ExerciseMapping_ModelToViewModelDefaultValue_Valid()
        {
            //Arrange
            _model = new Exercise();

            //Act
            _viewModel = _mapper.GetViewModel(_model);

            //Assert
            Assert.AreEqual(_model.Id,_viewModel.Id);
            Assert.AreEqual(default(string), _viewModel.Name);
            Assert.AreEqual(default(string), _viewModel.NameEnglish);
            Assert.AreEqual(default(string), _viewModel.NameFrench);
            Assert.AreEqual(default(int), _viewModel.MuscleId);
            Assert.AreEqual(default(string), _viewModel.MuscleName);
            Assert.AreEqual(0, _viewModel.ListMuscles.Count());
            Assert.AreEqual(0, _viewModel.WorkoutSessionExercices.Count());
        }

        [TestMethod]
        public void ExerciseMapping_ModelToViewModelAllFieldsWithValues_Valid()
        {
            //Arrange
            _model = new Exercise();
            _model.Id = 100;
            _model.Muscle = new Muscle { Id = 342, Name = "Bicep" };
            _model.Name = "Pushup";
            _model.WorkoutSessionExercices = new Collection<WorkoutSessionExercise> { new WorkoutSessionExercise(), new WorkoutSessionExercise() };
          

            //Act
            _viewModel = _mapper.GetViewModel(_model);

            //Assert
            Assert.AreEqual(_model.Id, _viewModel.Id);
            Assert.AreEqual(_model.Muscle.Id, _viewModel.MuscleId);
            Assert.AreEqual(_model.Muscle.Name.ToString(), _viewModel.MuscleName);
            Assert.AreEqual(_model.Name.ToString(), _viewModel.Name);
            Assert.AreEqual(_model.Name.English, _viewModel.NameEnglish);
            Assert.AreEqual(_model.Name.French, _viewModel.NameFrench);
            Assert.AreEqual(_model.WorkoutSessionExercices.Count, _viewModel.WorkoutSessionExercices.Count());

        }

        [TestMethod]
        public void ExerciseMapping_ErrorDirectPropertyFromModelToViewModel()
        {
            //Arrange
            _model = new Exercise();
            _model.Id = 100;
            _model.Muscle = new Muscle { Id = 342, Name = "Bicep" };
            _viewModel = _mapper.GetViewModel(_model);

            //Act
            var mapper = new MapperFactory();
            var translated = mapper.GetMapper(_model, _viewModel).GetErrorPropertyMappedFor(d => d.Name);
            

            //Assert
            Assert.AreEqual("Name", translated);
        }

        [TestMethod]
        public void ExerciseMapping_ErrorSubPropertyFromModelToViewModel()
        {
            //Arrange
            _model = new Exercise();
            _model.Id = 100;
            _model.Muscle = new Muscle { Id = 342, Name = "Bicep" };
            _model.Name = new LocalizedString();
            _viewModel = _mapper.GetViewModel(_model);

            //Act
            var mapper = new MapperFactory();
            var translated = mapper.GetMapper(_model, _viewModel).GetErrorPropertyMappedFor(d => d.Name.French);


            //Assert
            Assert.AreEqual("NameFrench", translated);
        }
    }
}
