using System;
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

        TY Map<T, TY>(T source, TY destination);
        TY Map<T, TY>(T source);
        ModelViewModelMapper<T, T1> GetMapper<T, T1>(T model, T1 viewModel);
        ModelViewModelMapper<TModel, TViewModel> GetMapper<TModel, TViewModel>();
    }
}