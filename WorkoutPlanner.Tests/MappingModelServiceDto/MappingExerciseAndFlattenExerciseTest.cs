using System.Collections.ObjectModel;
using Dto;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Setup.Ioc;

namespace WorkoutPlanner.Tests.MappingModelServiceDto
{
    [TestClass]
    public class MappingExerciseAndFlattenExerciseTest
    {
        private Exercise _model;
        private FlattenExercise _dto;
        private MapperService.Implementations.ExerciseMapper _mapper;
        public MappingExerciseAndFlattenExerciseTest()
        {
            UnityConfiguration.Initialize();
            var tt = UnityConfiguration.Container.Resolve<MapperService.Factory.IMapperFactory>();
            MapperService.Base.MapperConfiguration.Initialize(tt);
        }

        [TestInitialize]
        public void Initialize()
        {
            _model = new Exercise();
            _mapper = new MapperService.Implementations.ExerciseMapper();
        }


        [TestMethod]
        public void ExerciseMapping_ModelToDtoDefaultValue_Valid()
        {
            //Arrange
            _model = new Exercise();

            //Act
            _dto = _mapper.GetDto(_model);

            //Assert
            Assert.AreEqual(_model.Id, _dto.UniqueIdentifier);
            Assert.AreEqual(default(string), _dto.MuscleEnglishName);
            Assert.AreEqual(default(string), _dto.MuscleFrenchName);
            Assert.AreEqual(default(int), _dto.MuscleUniqueIdentifier);
            Assert.AreEqual(default(string), _dto.EnglishName);
            Assert.AreEqual(default(string), _dto.FrenchName);

        }

        [TestMethod]
        public void ExerciseMapping_ModelToDtoAllFieldsWithValues_Valid()
        {
            //Arrange
            _model = new Exercise();
            _model.Id = 100;
            _model.Muscle = new Muscle { Id = 342, Name = "Bicep" };
            _model.Name = "Pushup";
            _model.WorkoutSessionExercices = new Collection<WorkoutSessionExercise> { new WorkoutSessionExercise(), new WorkoutSessionExercise() };


            //Act
            _dto = _mapper.GetDto(_model);

            //Assert
            Assert.AreEqual(_model.Id, _dto.UniqueIdentifier);
            Assert.AreEqual(_model.Muscle.Id, _dto.MuscleUniqueIdentifier);
            Assert.AreEqual(_model.Name.English, _dto.EnglishName);
            Assert.AreEqual(_model.Name.French, _dto.FrenchName);
            Assert.AreEqual(_model.Muscle.Name.English, _dto.MuscleEnglishName);
            Assert.AreEqual(_model.Muscle.Name.French, _dto.MuscleFrenchName);

        }

    }
}
