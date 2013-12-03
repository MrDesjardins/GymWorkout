using Model;
using Services.Base;
using ViewModels;

namespace Services.Definitions
{
    public interface IWorkoutService : IService<Workout>
    {
        int UpdateSessionOrderOnly(Workout workout);
    }
}