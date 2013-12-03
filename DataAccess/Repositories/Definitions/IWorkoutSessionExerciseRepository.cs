using System.Collections.Generic;
using System.Linq;
using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;

namespace WorkoutPlanner.Repositories.Definitions
{
    public interface IWorkoutSessionExerciseRepository:IRepository<WorkoutSessionExercise>
    {
        IQueryable<WorkoutSessionExercise> GetForWorkoutSession(int workoutSessionId);
    }
}