using System.Collections.Generic;
using Model;
using Services.Base;
using ViewModels;

namespace Services.Definitions
{
    public interface IWorkoutSessionExerciseService : IService<WorkoutSessionExercise>
    {
        WorkoutSessionViewModel GetWorkoutSessionWithWorkoutSessionExercise(int workoutSessionId);
        int UpdatePartial(WorkoutSessionExercise model);
    }
}