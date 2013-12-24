using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Definitions;
using System.Linq;

namespace Model
{
    public class WorkoutSession : BaseModel, IUserOwnable
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public ICollection<WorkoutSessionExercise> WorkoutSessionExercises { get; set; }
        public virtual Workout Workout { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("WorkoutSession name is mandatory", new[] {"Name"});
            }

            if (Workout == null)
            {
                yield return new ValidationResult("Workout session must be associated to a workout");
            }

            if (WorkoutSessionExercises != null)
            {
                if (WorkoutSessionExercises.GroupBy(x => x.Order).Any(g => g.Count() > 1))
                {
                    yield return new ValidationResult("Every exercise must have a unique order of execution");
                }
            }
        }

        #region Implementation of IUserOwnable

        public string UserId { get; set; }

        #endregion
    }
}