using System.Collections.Generic;
using AutoMapper;
using Mappers.Base;
using Model;
using Model.Complex;
using ViewModels;
using ViewModels.Selectors.Implementations;

namespace Mappers.Implementations
{
    public class ExerciseMapper : ModelViewModelMapper<Exercise, ExerciseViewModel>
    {
        public ExerciseMapper()
        {
           
            base.AddModelViewModelToErrorsMap(d=>d.Id, d=>d.Id);
            base.AddModelViewModelToErrorsMap(d=>d.Name, d=>d.Name);
            base.AddModelViewModelToErrorsMap(d=>d.Name.French, d=>d.NameFrench);
            base.AddModelViewModelToErrorsMap(d=>d.Name.English, d=>d.NameEnglish);
            base.AddModelViewModelToErrorsMap(d=>d.Muscle.Name, d=>d.MuscleName);
        }

        #region Implementation of ModelViewModelMapper<Workout,WorkoutViewModel>

       

        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<Exercise, ExerciseViewModel>()
                    .ForMember(d => d.MuscleName, option => option.MapFrom(e => e.Muscle.Name))
                    .ForMember(d=>d.NameFrench,option=>option.MapFrom(e=>e.Name.French))
                    .ForMember(d=>d.NameEnglish,option=>option.MapFrom(e=>e.Name.English))
                    ;
            Mapper.CreateMap<ExerciseViewModel, Exercise>()
                .ForMember(d => d.Muscle, option => option.MapFrom(e => new Muscle { Name = e.MuscleName, Id = e.MuscleId}))
                .ForMember(d => d.Name, option => option.Ignore())
                .AfterMap((viewModel, model) =>
                              {
                                  model.Name = new LocalizedString() {English = viewModel.NameEnglish, French = viewModel.NameFrench};
                              })
                ;

            Mapper.CreateMap<Exercise, ExerciseSelector>()
                .ForMember(d => d.Value, option => option.MapFrom(e => e.Id))
                .ForMember(d => d.DisplayText, option => option.MapFrom(e => e.Name))
                ;
        }

        #endregion
    }
}