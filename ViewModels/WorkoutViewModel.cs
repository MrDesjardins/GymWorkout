using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class WorkoutViewModel : IViewModel
    {
        public WorkoutViewModel()
        {
            SavedMessage = null;
        }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Name { get; set; }
        public string Goal { get; set; }


        public string SessionsString { get; set; }

        #region Implementation of IViewModel

        public int Id { get; set; }

        #endregion

        [UIHint("SessionContainer")]
        public IEnumerable<WorkoutSessionViewModel> Sessions { get; set; }

        [UIHint("ExerciseCloneSelector")]
        public IEnumerable<ExerciseViewModel> AvailablesExercise { get; set; }

        public string SavedMessage { get; set; }
    }
}