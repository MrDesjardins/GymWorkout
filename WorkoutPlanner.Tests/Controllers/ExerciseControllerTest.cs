using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Sessions;
using BusinessLogic.Validations;
using DataAccessLayer;
using Mappers;
using Mappers.Base;
using Mappers.Factory;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Services.Base;
using Services.Definitions;
using Services.Implementations;
using Setup.Ioc;
using ViewModels;
using ViewModels.Selectors.Implementations;
using WorkoutPlanner.Controllers;
using WorkoutPlanner.Ioc;
using WorkoutPlanner.Validations;

namespace WorkoutPlanner.Tests.Controllers
{
    [TestClass]
    public class ExerciseControllerTest
    {
        private ExerciseController _controller;
        private Mock<IServiceFactory> _serviceFactoryMock;
        private Mock<IExerciseService> _exerciseService;
        private Mock<IMuscleService> _muscleService;

        public ExerciseControllerTest()
        {
            UnityConfiguration.Initialize();
        }

        [TestInitialize]
        public void Initialize()
        {
            //Create mock
            var userDTO = new UserSessionDTO {UserId = "1"};
            var userProfile = new ApplicationUser{UserId = "1"};
            
            _serviceFactoryMock = new Mock<IServiceFactory>();
            var userProviderMock = new Mock<IUserProvider>();
            var sessionHandlerMock = new Mock<ISessionHandler>();
            //var userSessionDTOMapperMock = new Mock<IUserSessionDTOMapper>();
            
            _exerciseService = new Mock<IExerciseService>();
            _muscleService = new Mock<IMuscleService>();

            //Initialize mock
            userProviderMock.Setup(d => d.Account).Returns(userProfile);
            sessionHandlerMock.Setup(d => d.GetUser()).Returns(userDTO);
            //userSessionDTOMapperMock.Setup(d => d.GetModel(userDTO)).Returns(userProfile);
            _serviceFactoryMock.Setup(d => d.Exercise).Returns(_exerciseService.Object);
            _serviceFactoryMock.Setup(d => d.Muscle).Returns(_muscleService.Object);

            //Register mock
            UnityConfiguration.Container.RegisterInstance(_serviceFactoryMock.Object);
            UnityConfiguration.Container.RegisterInstance(userProviderMock.Object);
            UnityConfiguration.Container.RegisterInstance(sessionHandlerMock);
            //UnityConfiguration.Container.RegisterInstance(userSessionDTOMapperMock.Object);

            MapperConfiguration.Initialize(UnityConfiguration.Container.Resolve<IMapperFactory>());
            _controller = UnityConfiguration.Container.Resolve<ExerciseController>();
            
        }

        [TestMethod]
        public void ExerciseController_IndexReturnModelList_ListNotEmpty()
        {
            //Arrange
            _exerciseService.Setup(d => d.GetAll()).Returns(new List<Exercise> { new Exercise() });

            //Act
            var result = _controller.Index();

            //Assert
            var view = (ViewResult)result;
            var model = (IEnumerable<ExerciseViewModel>) view.Model;
            Assert.AreEqual(1,model.Count());

        }

        [TestMethod]
        public void ExerciseController_DetailsReturnSingleModel_ModelValid()
        {
            //Arrange
            _exerciseService.Setup(d => d.Get(It.IsAny<Exercise>())).Returns(new Exercise { Id = 1 });
            _muscleService.Setup(d => d.GetAll()).Returns(new List<Muscle>());

            //Act
            var result = _controller.Details(1);

            //Assert
            var view = (ViewResult)result;
            var model = (ExerciseViewModel)view.Model;
            Assert.AreEqual(1,model.Id);
        }

        [TestMethod]
        public void ExerciseController_CreateModelStateValidAndValidationValid_RedirectWithNewId()
        {
            //Arrange
            _exerciseService.Setup(d => d.Create(It.IsAny<Exercise>())).Returns(new Exercise { Id = 1 });
            _exerciseService.Setup(d => d.Get(It.IsAny<Exercise>())).Returns(new Exercise { Id = 1 });
            _serviceFactoryMock.Setup(d => d.Muscle).Returns(_muscleService.Object);


            //Act
            var result = _controller.Create(new ExerciseViewModel{Id=0});//0 because not assigned yet

            //Assert
            var view = (RedirectToRouteResult)result;

            var createId = view.RouteValues.First(d=>d.Key=="Id").Value;
            var actionName = view.RouteValues.First(d=>d.Key=="action").Value;
            Assert.AreEqual(1,createId);
            Assert.AreEqual("Details", actionName);
        }

        [TestMethod]
        public void ExerciseController_CreateModelStateValidAndValidationInvalid_ReturnToCreateView()
        {
            //Arrange
            const string propertyName = "Property123";
            const string errorInTest = "Error in test";

            _exerciseService.Setup(d => d.Create(It.IsAny<Exercise>())).Throws(new ValidationErrors(new PropertyError(propertyName, errorInTest)));
            _exerciseService.Setup(d => d.Get(It.IsAny<Exercise>())).Returns(new Exercise { Id = 1 });



            _muscleService.Setup(d => d.GetAllSelector()).Returns(new List<MuscleSelector> 
                                                                                    {
                                                                                        new MuscleSelector("Chest", "Chest", true) 
                                                                                    });
            _serviceFactoryMock.Setup(d => d.Muscle).Returns(_muscleService.Object);

            //Act
            var result = _controller.Create(new ExerciseViewModel { Id = 0 });//0 because not assigned yet

            //Assert
            var view = (ViewResult)result;
            var model = (ExerciseViewModel)view.Model;
            Assert.AreEqual(0, model.Id);
            Assert.AreEqual(1, _controller.ModelState.Count);
            Assert.AreEqual(propertyName, _controller.ModelState.Keys.First());
            Assert.AreEqual(errorInTest, _controller.ModelState.Values.First().Errors.First().ErrorMessage);
            
        }

        [TestMethod]
        public void ExerciseController_CreateView_ViewModelWithListOfMuscle()
        {
            //Arrange


            _exerciseService.Setup(d => d.New()).Returns(new Exercise { Id = 1, Name = "ExerciseName", Muscle = new Muscle { Id = 1 }, WorkoutSessionExercices = null });
            _muscleService.Setup(d => d.GetAllSelector()).Returns(new List<MuscleSelector> 
                                                                                    {
                                                                                        new MuscleSelector("1", "Chest", true) ,
                                                                                         new MuscleSelector("2", "Bicep", false) 
                                                                                    });
            _serviceFactoryMock.Setup(d => d.Muscle).Returns(_muscleService.Object);

            //Act
            var result = _controller.Create();

            //Assert
            var view = (ViewResult)result;
            var viewModel = (ExerciseViewModel)view.Model;
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreEqual("ExerciseName", viewModel.Name);
            Assert.AreEqual(0, _controller.ModelState.Count);
            Assert.AreEqual(2, viewModel.ListMuscles.Count());

        }

    }
}
