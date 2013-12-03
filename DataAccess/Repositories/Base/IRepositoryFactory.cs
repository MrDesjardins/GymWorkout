using WorkoutPlanner.Repositories.Definitions;

namespace WorkoutPlanner.Repositories.Base
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