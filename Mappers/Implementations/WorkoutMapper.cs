using System.Collections.Generic;
using AutoMapper;
using Mappers.Base;
using Model;
using ViewModels;

namespace Mappers.Implementations
{
    public class WorkoutMapper : ModelViewModelMapper<Workout, WorkoutViewModel>
    {
        public WorkoutMapper()
        {
            base.AddModelViewModelToErrorsMap(d=>d.Goal, d=>d.Goal);
            base.AddModelViewModelToErrorsMap(d=>d.StartTime, d=>d.StartTime);
            base.AddModelViewModelToErrorsMap(d=>d.Name, d=>d.Name);
            base.AddModelViewModelToErrorsMap(d=>d.EndTime, d=>d.EndTime);
        } 

        #region Implementation of ModelViewModelMapper<Workout,WorkoutViewModel>



        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<Workout, WorkoutViewModel>()
               
                ;
            Mapper.CreateMap<WorkoutViewModel, Workout>()
         
                ;
        }

        #endregion
    }
}