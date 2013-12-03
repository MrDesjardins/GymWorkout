using DataAccessLayer.Repositories.Definitions;

namespace DataAccessLayer.Repositories.Base
{
    public interface IRepositoryFactory
    {
        IUserProfileRepository UserProfile { get; }
        IWorkoutRepository Workout { get;  }
        IWorkoutSessionRepository WorkoutSession { get;  }
        IWorkoutSessionExerciseRepository WorkoutSessionExercise { get;  }
        IExerciseRepository Exercise { get; }
        IMuscleRepository Muscle { get; }

    }
}