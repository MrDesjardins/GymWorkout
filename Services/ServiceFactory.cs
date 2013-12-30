using DataAccessLayer;
using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;
using Model.Definitions;
using Services.Base;
using Services.Definitions;
using Services.Implementations;

namespace Services
{
    public class ServiceFactory : IServiceFactory
    {
        private IUserProvider _userProvider;
        #region Implementation of IServiceFactory

        public IAccountService Account { get; private set; }
        public IMuscleService Muscle { get; private set; }
        public IWorkoutService Workout { get; private set; }
        public IWorkoutSessionService WorkoutSession { get; private set; }
        public IWorkoutSessionExerciseService WorkoutSessionExercise { get; private set; }


        public IExerciseService Exercise { get; private set; }

        #endregion

        public ServiceFactory(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory, IUserProvider userProvider)
        {
            _userProvider = userProvider;
            var account = _userProvider.Account;
            Account = new ApplicationUserService(repositoryFactory, mapperFactory);
            Muscle = new MuscleService(repositoryFactory, mapperFactory, account);
            Workout = new WorkoutService(repositoryFactory, mapperFactory, account);
            WorkoutSession = new WorkoutSessionService(repositoryFactory, mapperFactory, account);
            WorkoutSessionExercise = new WorkoutSessionExerciseService(repositoryFactory, mapperFactory, account);
            Exercise = new ExerciseService(repositoryFactory, mapperFactory, Muscle, account);
        }
    }
}