using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BusinessLogic;
using DataAccessLayer;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Definitions;
using DataAccessLayer.Repositories.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Complex;
using Moq;
using WorkoutPlanner.Database;

namespace WorkoutPlanner.Tests.Repository
{
    [TestClass]
    public class ExerciseRepositoryTest
    {

        private Exercise _entity;
        private readonly IExerciseRepository _repository;
        private readonly IExerciseRepository _repositoryValidation;
        private readonly Mock<IUserProvider> _userProvider;

        public ExerciseRepositoryTest()
        {
            _userProvider = new Mock<IUserProvider>(MockBehavior.Strict);
            _userProvider.Setup(d => d.Account).Returns(new ApplicationUser { UserId = "1" });
            var db = new DatabaseContext();
            db.CurrentUser = _userProvider.Object.Account;
            _repository = new ExerciseRepository(db);
            _repositoryValidation = new ExerciseRepository(db);
            
        }

        [TestInitialize]
        public void Initialize()
        {
            _entity = new Exercise();
        }

        [TestMethod]
        public void ExerciceRepositoryTestCreateNewExercise_InsertWithNotNullMuscle_Valid()
        {
            //Arrange
            _entity = new Exercise { Name = new LocalizedString { French = "Test unitaire", English = "Unit Test" }, Muscle = new Muscle { Id = 3} };

            //Act
            using (var scope = new TransactionScope())
            {
                _repository.Insert(_entity);

                //Assert
                var repositoryObject = _repositoryValidation.Get(_entity.Id);
            }
        }

        [TestMethod]
        public void ExerciceRepositoryTestCreateNewExercise_InsertWithNotMuscle_Valid()
        {
            //Arrange
            _entity = new Exercise { Name = new LocalizedString { French = "Test unitaire", English = "Unit Test" }, Muscle = null };

            //Act
            using (var scope = new TransactionScope())
            {
                try
                {
                    _repository.Insert(_entity);
                }
                catch (DatabaseValidationErrors)
                {
                    //Should go there because it calls the model validation which require to have a muscle
                }

            }
        }


        [TestMethod]
        public void ExerciceRepositoryTestUpdateNewExercise_Update_Valid()
        {
            const string NOM_MODIFIER = "Nom modifié";
            using (var scope = new TransactionScope())
            {
                //Arrange
                _entity = new Exercise {Name = new LocalizedString {French = "Test unitaire", English = "Unit Test"}, Muscle = new Muscle {Id = 3}};
                _repository.Insert(_entity);

                //Act
                _entity.Name.French = NOM_MODIFIER;
                _repository.Update(_entity);

                //Assert
                var repositoryObject = _repositoryValidation.Get(_entity.Id);
                Assert.AreEqual(NOM_MODIFIER, _entity.Name.French);
            }
        }
    }
}
