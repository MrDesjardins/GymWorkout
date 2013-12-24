using DataAccessLayer.Repositories.Definitions;
using Model.Definitions;

namespace DataAccessLayer.Repositories.Base
{
    public interface IRepositoryFactory
    {
        IWorkoutRepository Workout { get;  }
        IApplicationUserRepository ApplicationUser { get; }
        IWorkoutSessionRepository WorkoutSession { get;  }
        IWorkoutSessionExerciseRepository WorkoutSessionExercise { get;  }
        IExerciseRepository Exercise { get; }
        IMuscleRepository Muscle { get; }

        void SetUser(ICurrentUser user);
    }
}