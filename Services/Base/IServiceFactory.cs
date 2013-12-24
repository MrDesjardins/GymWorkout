using Model.Definitions;
using Services.Definitions;

namespace Services.Base
{
    public interface IServiceFactory
    {
        IAccountService Account { get; }
        IMuscleService Muscle { get; }
        IExerciseService Exercise { get; }
        IWorkoutService Workout { get; }
        IWorkoutSessionService WorkoutSession { get; }
        IWorkoutSessionExerciseService WorkoutSessionExercise { get; }
    }
}