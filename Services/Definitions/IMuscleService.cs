using System.Collections.Generic;
using Model;
using Services.Base;
using ViewModels.Selectors.Implementations;
using WorkoutPlanner.ViewModels;

namespace Services.Definitions
{
    public interface IMuscleService : IService<Muscle>
    {
        IEnumerable<MuscleSelector> GetAllSelector();
    }
}