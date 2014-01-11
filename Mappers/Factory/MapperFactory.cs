using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Sessions;
using Mappers.Base;
using Mappers.Definitions;
using Mappers.Implementations;
using Model;
using ViewModels;
using WorkoutPlanner.ViewModels;

namespace Mappers.Factory
{
    /// <summary>
    /// The factory allow two ways to access the mapper.
    /// 
    /// 1) With the property defined for each Model. For example MapperFactory.Exercise
    ///    -> This is useful when used in Service Layer
    /// 2) With the MapperFactory.GetMapper(typeModel, TypeViewModel)
    ///    -> This is useful when the BaseController want to see if it can map
    ///       error between Model and ViewModel (model is known by controller, view model by the model binder)
    /// </summary>
    public class MapperFactory : IMapperFactory
    {

        public List<ConcreteMapper> MapperProfiles { get; set; }


        public MapperFactory()
        {
            var definitions = InstanciateAllClassesOfBaseType<IMapper>().ToList();
            var concreteMappers = new List<ConcreteMapper>();
            foreach (var definition in definitions)
            {
                definition.Register();
                concreteMappers.Add(new ConcreteMapper(definition));
            }
            MapperProfiles = concreteMappers;
        }

        private static IEnumerable<T> InstanciateAllClassesOfBaseType<T>()
        {
            var instances = (from type in typeof(T).Assembly.GetTypes()
                             where typeof(T).IsAssignableFrom(type) && !type.IsAbstract
                             select type).Select(d => (T)Activator.CreateInstance(d))
                                         .ToArray();
            return instances;
        }
        public TY Map<T, TY>(T source, TY destination)
        {
            return Mapper.Map(source, destination);
        }

        public TY Map<T, TY>(T source)
        {
            TY destination = default(TY);
            return this.Map(source, destination);
        }

        /// <summary>
        /// Gets the mapper dynamically by type of the Model and ViewModel.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <param name="model">The model.</param>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public ModelViewModelMapper<TModel, TViewModel> GetMapper<TModel, TViewModel>(TModel model, TViewModel viewModel)
        {
            var mapper = this.MapperProfiles.Single(d => d.Model == model.GetType() && d.ViewModel == viewModel.GetType());

            return mapper.Profile as ModelViewModelMapper<TModel, TViewModel>;
        }

        public ModelViewModelMapper<TModel, TViewModel> GetMapper<TModel, TViewModel>()
        {
            Type modelType = typeof(TModel);
            Type viewModelType = typeof(TViewModel);
            var mapper = this.MapperProfiles
                                .Single(d => d.Model == modelType && d.ViewModel == viewModelType);

            return mapper.Profile as ModelViewModelMapper<TModel, TViewModel>;
        }

    
    }
}