using System.Linq;
using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;

namespace WorkoutPlanner.Repositories.Definitions
{
    public interface IWorkoutSessionRepository:IRepository<WorkoutSession>
    {
        IQueryable<WorkoutSession> GetAllForkWorkout(int workoutId);
    }
}