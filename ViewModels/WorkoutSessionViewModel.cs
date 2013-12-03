using System.Collections.Generic;

namespace ViewModels
{
    public class WorkoutSessionViewModel : IViewModel
    {
        public string Name { get; set; }
        public int Order { get; set; }
        #region Implementation of IViewModel

        public int Id { get; set; }

        #endregion

        public int WorkoutId { get; set; }

        
        public IEnumerable<WorkoutSessionExerciseViewModel> Exercises { get; set; }
    }
}