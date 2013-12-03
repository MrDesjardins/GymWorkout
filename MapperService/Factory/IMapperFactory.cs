using System.Collections.Generic;
using Dto;
using MapperService.Base;
using MapperService.Implementations;
using Model;

namespace MapperService.Factory
{
    /// <summary>
    /// Contain all mappers between all model and view model.
    /// </summary>
    public interface IMapperFactory
    {
        List<ConcreteMapper> MapperProfiles { get; set; }
        ModelDtoMapper<Exercise,FlattenExercise> Exercise { get; }

        TY Map<T, TY>(T source, TY destination);
        TY Map<T, TY>(T source);
    }
}