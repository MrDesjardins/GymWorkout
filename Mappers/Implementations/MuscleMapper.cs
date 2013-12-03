using System.Collections.Generic;
using AutoMapper;
using Mappers.Base;
using Model;
using Model.Complex;
using ViewModels.Selectors.Implementations;
using WorkoutPlanner.ViewModels;

namespace Mappers.Implementations
{
    public class MuscleMapper : ModelViewModelMapper<Muscle, MuscleViewModel>
    {

        public MuscleMapper()
        {
            base.AddModelViewModelToErrorsMap(d=>d.Name.French, d=>d.NameFrench);
            base.AddModelViewModelToErrorsMap(d=>d.Name.English, d=>d.NameEnglish);
        }

        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<Muscle, MuscleViewModel>()
                    .ForMember(d => d.Id, option=>option.MapFrom(s=>s.Id))
                    .ForMember(d => d.NameFrench, option=>option.MapFrom(s=>s.Name.French))
                    .ForMember(d => d.NameEnglish, option=>option.MapFrom(s=>s.Name.English))
                    ;
            Mapper.CreateMap<MuscleViewModel, Muscle>()
                .ForMember(d => d.Name, option => option.Ignore())
                .AfterMap((viewModel, model) =>
                {
                    model.Name = new LocalizedString() { French = viewModel.NameFrench, English = viewModel.NameEnglish};
                })
                ;

            Mapper.CreateMap<Muscle, MuscleSelector>()
           .ForMember(d => d.Value, option => option.MapFrom(e => e.Id))
           .ForMember(d => d.DisplayText, option => option.MapFrom(e => e.Name))
           ;
        }
    }
}