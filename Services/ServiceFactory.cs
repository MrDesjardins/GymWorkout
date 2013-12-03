using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;
using Services.Base;
using Services.Definitions;
using Services.Implementations;

namespace Services
{
    public class ServiceFactory : IServiceFactory
    {
        #region Implementation of IServiceFactory

        public IAccountService Account { get; private set; }
        public IMuscleService Muscle { get; private set; }
        public IWorkoutService Workout { get; private set; }
        public IWorkoutSessionService WorkoutSession { get; private set; }
        public IWorkoutSessionExerciseService WorkoutSessionExercise { get; private set; }
        public IExerciseService Exercise { get; private set; }

        #endregion

        public ServiceFactory(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory)
        {
            Account = new AccountService(repositoryFactory, mapperFactory);
            Muscle = new MuscleService(repositoryFactory, mapperFactory);
            Workout = new WorkoutService(repositoryFactory, mapperFactory);
            WorkoutSession = new WorkoutSessionService(repositoryFactory, mapperFactory);
            WorkoutSessionExercise = new WorkoutSessionExerciseService(repositoryFactory, mapperFactory);
            Exercise = new ExerciseService(repositoryFactory, mapperFactory, Muscle);
        }
    }
}