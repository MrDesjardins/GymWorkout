using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Sessions;
using Mappers.Base;
using Mappers.Definitions;
using Mappers.Implementations;
using Model;
using ViewModels;
using WorkoutPlanner.ViewModels;

namespace Mappers.Factory
{
    /// <summary>
    /// The factory allow two ways to access the mapper.
    /// 
    /// 1) With the property defined for each Model. For example MapperFactory.Exercise
    ///    -> This is useful when used in Service Layer
    /// 2) With the MapperFactory.GetMapper(typeModel, TypeViewModel)
    ///    -> This is useful when the BaseController want to see if it can map
    ///       error between Model and ViewModel (model is known by controller, view model by the model binder)
    /// </summary>
    public class MapperFactory : IMapperFactory
    {

        public List<ConcreteMapper> MapperProfiles { get; set; }

        private ModelViewModelMapper<Workout, WorkoutViewModel> _workout;
        private ModelViewModelMapper<WorkoutSession, WorkoutSessionViewModel> _workoutSession;
        private ModelViewModelMapper<WorkoutSessionExercise, WorkoutSessionExerciseViewModel> _workoutSessionExercise;
        private ModelViewModelMapper<Exercise, ExerciseViewModel> _exercise;
        private UserSessionDTOMapper _userSessionDTO;
        private ModelViewModelMapper<UserProfile, UserProfileViewModel> _userProfile; 
        private ModelViewModelMapper<Muscle, MuscleViewModel> _muscle; 

 
        #region Implementation of IMapperFactory

        public ModelViewModelMapper<Workout, WorkoutViewModel> Workout
        {
            get { return _workout ?? (_workout = new WorkoutMapper()); }
        }
        public ModelViewModelMapper<WorkoutSession, WorkoutSessionViewModel> WorkoutSession
        {
            get { return _workoutSession ?? (_workoutSession = new WorkoutSessionMapper()); }
        }
        public ModelViewModelMapper<WorkoutSessionExercise, WorkoutSessionExerciseViewModel> WorkoutSessionExercise
        {
            get { return _workoutSessionExercise ?? (_workoutSessionExercise = new WorkoutSessionExerciseMapper()); }
        }

        public ModelViewModelMapper<Exercise, ExerciseViewModel> Exercise
        {
            get { return _exercise ?? (_exercise = new ExerciseMapper()); }
        }

        public IUserSessionDTOMapper UserSessionDTO
        {
            get { return _userSessionDTO ?? (_userSessionDTO = new UserSessionDTOMapper()); }
        }

        public ModelViewModelMapper<UserProfile, UserProfileViewModel> UserProfile
        {
            get { return _userProfile ?? (_userProfile = new UserProfileMapper()); }
        }
        public ModelViewModelMapper<Muscle, MuscleViewModel> Muscle
        {
            get { return _muscle ?? (_muscle = new MuscleMapper()); }
        }

        public TY Map<T, TY>(T source, TY destination)
        {
            return Mapper.Map(source, destination);
        }

        public TY Map<T, TY>(T source)
        {
            TY destination = default(TY);
            return this.Map(source, destination);
        }

        /// <summary>
        /// Gets the mapper dynamically by type of the Model and ViewModel.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public ModelViewModelMapper<TModel, TViewModel> GetMapper<TModel, TViewModel>(TModel model, TViewModel viewModel)
        {
            var mapper = this.MapperProfiles.Single(d => d.Model == model.GetType() && d.ViewModel == viewModel.GetType());

            return mapper.Profile as ModelViewModelMapper<TModel, TViewModel>;
        }

        
        #endregion

        public MapperFactory()
        {
            this.MapperProfiles = new List<ConcreteMapper>();
            this.MapperProfiles.Add(new ConcreteMapper(typeof(Workout),typeof(WorkoutViewModel),this.Workout));
            this.MapperProfiles.Add(new ConcreteMapper(typeof(WorkoutSession),typeof(WorkoutSessionViewModel),this.WorkoutSession));
            this.MapperProfiles.Add(new ConcreteMapper(typeof(WorkoutSessionExercise),typeof(WorkoutSessionExerciseViewModel),this.WorkoutSessionExercise));
            this.MapperProfiles.Add(new ConcreteMapper(typeof(Exercise),typeof(ExerciseViewModel),this.Exercise));
            this.MapperProfiles.Add(new ConcreteMapper(typeof(UserProfile), typeof(UserSessionDTO), this.UserSessionDTO));
            this.MapperProfiles.Add(new ConcreteMapper(typeof(UserProfile), typeof(UserProfileViewModel), this.UserProfile));
            this.MapperProfiles.Add(new ConcreteMapper(typeof(Muscle), typeof(MuscleViewModel), this.Muscle));
        }
    }
}