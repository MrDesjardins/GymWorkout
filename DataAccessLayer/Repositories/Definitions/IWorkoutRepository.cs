using DataAccessLayer.Repositories.Base;
using Model;

namespace DataAccessLayer.Repositories.Definitions
{
    public interface IWorkoutRepository:IRepository<Workout>
    {
        int GetAmountWorkoutForCurrentMonth();
        int UpdateSessionOrderOnly(Workout workout);
    }
}