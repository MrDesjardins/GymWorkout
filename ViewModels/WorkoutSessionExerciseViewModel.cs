using System;
using System.Collections.Generic;
using ViewModels.Selectors.Implementations;

namespace ViewModels
{
    public class WorkoutSessionExerciseViewModel : IViewModel
    {


        #region Implementation of IViewModel

        public int Id { get; set; }

        #endregion

        public int WorkoutSessionId { get; set; }
        public int Order { get; set; }
        public string Repetitions { get; set; }
        public string Weights { get; set; }
        public string Tempo { get; set; }
        public Int64 RestBetweenSetTicks { get; set; }

        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }





        public IEnumerable<ExerciseSelector> ListExercise { get; set; }

        public string WorkoutSessionName { get; set; }
    }
}