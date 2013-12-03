using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Repositories.Base;
using Model;

namespace DataAccessLayer.Repositories.Definitions
{
    public interface IWorkoutSessionExerciseRepository:IRepository<WorkoutSessionExercise>
    {
        IQueryable<WorkoutSessionExercise> GetForWorkoutSession(int workoutSessionId);
        int UpdatePartial(WorkoutSessionExercise entity);
    }
}