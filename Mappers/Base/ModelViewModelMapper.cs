using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;

namespace Mappers.Base
{
    /// <summary>
    /// Map between model and view model. Main goal is to enable the mapping between error that is assigned
    /// to the model and must be mapped back to the view model to be able to have the error next to the
    /// control that represent the view model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    public abstract class ModelViewModelMapper<TModel, TViewModel>:ClassMapper
    {
        public ModelViewModelMapper()
        {
            PropertiesMap = new ModelViewModelPropertiesMap<TModel, TViewModel>();
        }

        public ModelViewModelMapper(ModelViewModelPropertiesMap<TModel, TViewModel> propertiesMap)
        {
            PropertiesMap = propertiesMap;
        }


        /// <summary>
        /// Gets or sets the properties map. This is required when we want to associate MODEL and VIEWMODEL
        /// property for error mapping.
        /// </summary>
        /// <value>
        /// The properties map.
        /// </value>
        private ModelViewModelPropertiesMap<TModel,TViewModel> PropertiesMap { get; set; }

        public IEnumerable<TViewModel> GetViewModelList(IEnumerable<TModel> modelList)
        {
            return Mapper.Map<IEnumerable<TModel>, IEnumerable<TViewModel>>(modelList);
        }

        public IEnumerable<TModel> GetModelList(IEnumerable<TViewModel> viewModelList)
        {
            return Mapper.Map<IEnumerable<TViewModel>, IEnumerable<TModel>>(viewModelList);
        }

        public TModel GetModel(TViewModel viewModel)
        {
            return Mapper.Map<TViewModel, TModel>(viewModel);
        }

        public TViewModel GetViewModel(TModel model)
        {
            return Mapper.Map<TModel, TViewModel>(model);
        }



        public string GetErrorPropertyMappedFor(Expression<Func<TModel, object>> property)
        {
            return PropertiesMap.GetErrorPropertyMappedFor(property);
        }

        /// <summary>
        /// This all the association between the Model and ViewModel. If defined, the Enhanced validation result
        /// is used and the error will be map between the model and the view model. Otherwise, the error is assigned
        /// to the model property name.
        /// </summary>
        /// <param name="propertyModel">The property model.</param>
        /// <param name="propertyViewModel">The property view model.</param>
        public void AddModelViewModelToErrorsMap(Expression<Func<TModel, object>> propertyModel, Expression<Func<TViewModel, object>> propertyViewModel)
        {
            PropertiesMap.AddModelViewModelToErrorsMap(propertyModel, propertyViewModel);
        }
    }
}