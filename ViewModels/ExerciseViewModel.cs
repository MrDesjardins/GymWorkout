using System.Collections.Generic;
using ViewModels.Selectors.Implementations;
using WorkoutPlanner.ViewModels;

namespace ViewModels
{
    public class ExerciseViewModel : IViewModel
    {
        public string Name { get; set; }
        public string NameFrench { get; set; }
        public string NameEnglish { get; set; }

        public int MuscleId { get; set; }
        public string MuscleName { get; set; }

        #region Implementation of IViewModel

        public int Id { get; set; }

        #endregion


        public IEnumerable<WorkoutSessionExerciseViewModel> WorkoutSessionExercices { get; set; }
        public IEnumerable<MuscleSelector> ListMuscles { get; set; }

        public ExerciseViewModel()
        {
            this.WorkoutSessionExercices = new List<WorkoutSessionExerciseViewModel>();
            this.ListMuscles = new List<MuscleSelector>();
        }
    }
}