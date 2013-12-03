using System.Collections.Generic;
using Mappers.Base;
using Mappers.Definitions;
using Model;
using ViewModels;
using WorkoutPlanner.ViewModels;

namespace Mappers.Factory
{
    /// <summary>
    /// Contain all mappers between all model and view model.
    /// </summary>
    public interface IMapperFactory
    {
        List<ConcreteMapper> MapperProfiles { get; set; }

        ModelViewModelMapper<Workout, WorkoutViewModel> Workout { get; }
        ModelViewModelMapper<WorkoutSession, WorkoutSessionViewModel> WorkoutSession { get; }
        ModelViewModelMapper<WorkoutSessionExercise, WorkoutSessionExerciseViewModel> WorkoutSessionExercise { get; }
        ModelViewModelMapper<Exercise, ExerciseViewModel> Exercise { get; }
        ModelViewModelMapper<UserProfile, UserProfileViewModel> UserProfile { get; }
        IUserSessionDTOMapper UserSessionDTO { get; }
        ModelViewModelMapper<Muscle, MuscleViewModel> Muscle { get; }

        TY Map<T, TY>(T source, TY destination);
        TY Map<T, TY>(T source);
        ModelViewModelMapper<T, T1> GetMapper<T, T1>(T model, T1 viewModel);
    }
}