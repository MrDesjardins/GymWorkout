using System.Collections.Generic;
using WorkoutPlanner.Validations;

namespace BusinessLogic.Validations
{
    public interface IValidationErrors
    {
        List<IBaseError> Errors { get; set; }
    }
}