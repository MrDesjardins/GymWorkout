using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Definitions;

namespace Model
{
    public class Workout : BaseModel, IUserOwnable
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [Required(ErrorMessage = "Name is mandatory from DataAnnotation")]
        public string Name { get; set; }
        public string Goal { get; set; }
        public ICollection<WorkoutSession> Sessions { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is mandatory", new[] {"Name"});
            }
            if (EndTime.HasValue)
            {
                if (StartTime > EndTime.Value)
                {
                    yield return new ValidationResult("EndTime must be after the StartTime", new[] {"StartTime", "EndTime"});
                }
            }
        }

        #region Implementation of IUserOwnable

        public int UserId { get; set; }

        #endregion
    }
}