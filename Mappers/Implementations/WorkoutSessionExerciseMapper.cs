using System.Collections.Generic;
using AutoMapper;
using Mappers.Base;
using Model;
using ViewModels;

namespace Mappers.Implementations
{
    public class WorkoutSessionExerciseMapper : ModelViewModelMapper<WorkoutSessionExercise, WorkoutSessionExerciseViewModel>
    {
        #region Implementation of ModelViewModelMapper<Workout,WorkoutViewModel>


        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<WorkoutSessionExercise, WorkoutSessionExerciseViewModel>()
                    .ForMember(d => d.ExerciseId, option => option.MapFrom(e => e.Exercise.Id))
                    .ForMember(d => d.ExerciseName, option => option.MapFrom(e => e.Exercise.Name))
                    .ForMember(d => d.WorkoutSessionId,option=>option.MapFrom(e => e.WorkoutSession.Id))
                    .ForMember(d => d.WorkoutSessionName,option=>option.MapFrom(e => e.WorkoutSession.Name))
                    .ForMember(d => d.ListExercise, option=>option.Ignore())
                    ;
            Mapper.CreateMap<WorkoutSessionExerciseViewModel, WorkoutSessionExercise>()
                .ForMember(d => d.WorkoutSession, option => option.MapFrom(e => new WorkoutSession { Id = e.WorkoutSessionId }))
                .ForMember(d => d.Exercise, option => option.Ignore())
                .AfterMap((viewModel, model) => {
                    model.Exercise = new Exercise { Id = viewModel.ExerciseId, Name = viewModel.ExerciseName };
                })
                ;
        }

        #endregion
    }
}