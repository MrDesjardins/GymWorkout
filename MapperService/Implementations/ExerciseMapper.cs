using AutoMapper;
using Dto;
using MapperService.Base;
using Model;

namespace MapperService.Implementations
{
    public class ExerciseMapper : ModelDtoMapper<Exercise, FlattenExercise>
    {
        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<Exercise, FlattenExercise>()
                    .ForMember(d => d.FrenchName, option => option.MapFrom(e => e.Name.French))
                    .ForMember(d => d.EnglishName, option => option.MapFrom(e => e.Name.English))
                    .ForMember(d => d.MuscleFrenchName, option => option.MapFrom(e => e.Muscle.Name.French))
                    .ForMember(d => d.MuscleEnglishName, option => option.MapFrom(e => e.Muscle.Name.English))
                    .ForMember(d => d.MuscleUniqueIdentifier, option => option.MapFrom(e => e.Muscle.Id))
                    .ForMember(d => d.UniqueIdentifier, option => option.MapFrom(e => e.Id))
                    ;
        }
    }
}
