using System.Collections.Generic;
using AutoMapper;
using Mappers.Base;
using Model;
using ViewModels;

namespace Mappers.Implementations
{
    public class WorkoutSessionMapper : ModelViewModelMapper<WorkoutSession, WorkoutSessionViewModel>
    {
        #region Implementation of ModelViewModelMapper<Workout,WorkoutViewModel>


        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<WorkoutSession, WorkoutSessionViewModel>()
                .ForMember(d => d.WorkoutId, option => option.MapFrom(e => e.Workout.Id))
                .ForMember(d =>d.Exercises , option=>option.MapFrom(e=>e.WorkoutSessionExercises))
                ;
            Mapper.CreateMap<WorkoutSessionViewModel, WorkoutSession>()
                .ForMember(d => d.Workout, option => option.MapFrom(e => new Workout { Id = e.WorkoutId }))
                .ForMember(d => d.WorkoutSessionExercises, option => option.MapFrom(e => e.Exercises))
                ;
        }

        #endregion
    }
}