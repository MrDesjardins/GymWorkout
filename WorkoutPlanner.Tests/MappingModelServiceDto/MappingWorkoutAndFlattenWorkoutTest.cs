using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Setup.Ioc;

namespace WorkoutPlanner.Tests.MappingModelServiceDto
{
    [TestClass]
    public class MappingWorkoutAndFlattenWorkoutTest
    {
        private Workout _model;
        private FlattenWorkout _dto;
        private MapperService.Implementations.WorkoutMapper _mapper;
        public MappingWorkoutAndFlattenWorkoutTest()
        {
            UnityConfiguration.Initialize();
            var tt = UnityConfiguration.Container.Resolve<MapperService.Factory.IMapperFactory>();
            MapperService.Base.MapperConfiguration.Initialize(tt);
        }

        [TestInitialize]
        public void Initialize()
        {
            _model = new Workout();
            _mapper = new MapperService.Implementations.WorkoutMapper();
        }


        [TestMethod]
        public void WorkoutMapping_ModelToDtoDefaultValue_Valid()
        {
            //Arrange
            _model = new Workout();

            //Act
            _dto = _mapper.GetDto(_model);

            //Assert
            Assert.AreEqual(_model.Id, _dto.UniqueIdentifier);
            Assert.AreEqual(default(string), _dto.Name);
            Assert.AreEqual(default(string), _dto.GoalDescription);
            Assert.AreEqual(default(DateTime), _dto.StartTime);
            Assert.AreEqual(default(DateTime), _dto.EndTime);

        }

        [TestMethod]
        public void WorkoutMapping_ModelToDtoAllFieldsWithValues_Valid()
        {
            //Arrange
            _model = new Workout();
            _model.Id = 100;
            _model.Name = "Test Name Workout";
            _model.Goal = "Make more pushup";
            _model.StartTime = new DateTime(2013, 08, 26);
            _model.EndTime = new DateTime(2013, 10, 26);


            //Act
            _dto = _mapper.GetDto(_model);

            //Assert
            Assert.AreEqual(_model.Id, _dto.UniqueIdentifier);
            Assert.AreEqual(_model.Name, _dto.Name);
            Assert.AreEqual(_model.Goal, _dto.GoalDescription);
            Assert.AreEqual(_model.StartTime, _dto.StartTime);
            Assert.AreEqual(_model.EndTime, _dto.EndTime);
  

        }
    }
}
