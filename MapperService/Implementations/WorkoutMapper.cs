using AutoMapper;
using Dto;
using MapperService.Base;
using Model;

namespace MapperService.Implementations
{
    public class WorkoutMapper : ModelDtoMapper<Workout, FlattenWorkout>
    {
        protected override void Configure()
        {
            base.Configure();
            Mapper.CreateMap<Workout, FlattenWorkout>()
                  .ForMember(d => d.Name, option => option.MapFrom(e => e.Name))
                  .ForMember(d => d.GoalDescription, option => option.MapFrom(e => e.Goal))
                  .ForMember(d => d.StartTime, option => option.MapFrom(e => e.StartTime))
                  .ForMember(d => d.EndTime, option => option.MapFrom(e => e.EndTime))
                  .ForMember(d => d.UniqueIdentifier, option => option.MapFrom(e => e.Id))
                ;
        }
    }
}