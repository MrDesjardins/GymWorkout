using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Model.Definitions;

namespace Model
{
    public class WorkoutSessionExercise : BaseModel, IUserOwnable
    {
        public int Order { get; set; }
        public string Repetitions { get; set; }
        public string Weights { get; set; }
        public string Tempo { get; set; }
        public Int64 RestBetweenSetTicks { get; set; }

        public TimeSpan RestBetweenExercices
        {
            get { return TimeSpan.FromTicks(RestBetweenSetTicks); }
            set { RestBetweenSetTicks = value.Ticks; }
        }

        public virtual Exercise Exercise { get; set; }
        public virtual WorkoutSession WorkoutSession { get; set; }


        public WorkoutSessionExercise()
        {
            RestBetweenSetTicks = NOT_INITIALIZED;
            Order = NOT_INITIALIZED;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new Collection<ValidationResult>();
        }

        #region Implementation of IUserOwnable

        public int UserId { get; set; }

        #endregion
    }
}