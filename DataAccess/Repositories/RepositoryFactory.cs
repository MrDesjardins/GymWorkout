using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkoutPlanner.Database;
using WorkoutPlanner.Repositories.Base;
using WorkoutPlanner.Repositories.Definitions;
using WorkoutPlanner.Repositories.Implementations;

namespace WorkoutPlanner.Repositories
{
    public class RepositoryFactory:IRepositoryFactory
    {
        private readonly IDatabaseContext _databaseContext;
        private IUserProfileRepository _userProfileRepository;
        private IWorkoutRepository _workoutRepository;
        private IWorkoutSessionRepository _workoutSessionRepository;
        private IWorkoutSessionExerciseRepository _workoutSessionExerciseRepository;
        private IExerciseRepository _exerciseRepository;
        private IMuscleRepository _muscleRepository;

        public RepositoryFactory(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #region Implementation of IRespositoryFactory

        public IUserProfileRepository UserProfile
        {
            get { return _userProfileRepository ?? (_userProfileRepository = new UserProfileRepository(_databaseContext)); }
        }

        public IWorkoutRepository Workout
        {
            get { return _workoutRepository ?? (_workoutRepository = new WorkoutRepository(_databaseContext)); }
        }

        public IWorkoutSessionRepository WorkoutSession 
        { 
            get { return _workoutSessionRepository ?? (_workoutSessionRepository = new WorkoutSessionRepository(_databaseContext)); }
        }

        public IWorkoutSessionExerciseRepository WorkoutSessionExercise
        {
            get { return _workoutSessionExerciseRepository ?? (_workoutSessionExerciseRepository = new WorkoutSessionExerciseRepository(_databaseContext)); }
        }

        public IExerciseRepository Exercise
        {
            get { return _exerciseRepository ?? (_exerciseRepository = new ExerciseRepository(_databaseContext)); }
        }

        public IMuscleRepository Muscle
        {
            get { return _muscleRepository ?? (_muscleRepository = new MuscleRepository(_databaseContext)); }
        }

        #endregion
    }
}