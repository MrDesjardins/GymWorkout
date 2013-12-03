using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;

namespace WorkoutPlanner.Repositories.Definitions
{
    public interface IWorkoutRepository:IRepository<Workout>
    {
        int GetAmountWorkoutForCurrentMonth();
    }
}