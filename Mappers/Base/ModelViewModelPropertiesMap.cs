using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Mappers.Base
{
    /// <summary>
    /// Contains a list of property model-viewmodel and a list of errors.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    public class ModelViewModelPropertiesMap<TModel, TViewModel>
    {
        private Dictionary<string, string> ErrorsMap { get; set; }

        public ModelViewModelPropertiesMap()
        {
            ErrorsMap = new Dictionary<string, string>();
        }

        public ModelViewModelPropertiesMap(Dictionary<Expression<Func<TModel, object>>, Expression<Func<TViewModel, object>>> propertiesMap):this()
        {
            foreach (var keyValuePair in propertiesMap)
            {
                AddModelViewModelToErrorsMap(keyValuePair.Key, keyValuePair.Value);
            }
        }

        /// <summary>
        /// Return the 
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public string GetErrorPropertyMappedFor(Expression<Func<TModel, object>> property)
        {
            var name = GetPropertyName<TModel>(property);
            return this.ErrorsMap.ContainsKey(name) ? this.ErrorsMap[name] : name;
        }

        /// <summary>
        /// Adds the model view model to errors map. This will map the property Model Name to the property ViewModel Name.
        /// </summary>
        /// <param name="propertyModel">The property model.</param>
        /// <param name="propertyViewModel">The property view model.</param>
        public void AddModelViewModelToErrorsMap(Expression<Func<TModel, object>> propertyModel, Expression<Func<TViewModel, object>> propertyViewModel)
        {
            var name = GetPropertyName<TModel>(propertyModel);
            var nameViewModel = GetPropertyName<TViewModel>(propertyViewModel);

            ErrorsMap.Add(name, nameViewModel);
        }

        /// <summary>
        /// Gets the name of the property. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyModel">The property model which will be parsed to get the name of this one</param>
        /// <returns></returns>
        private static string GetPropertyName<T>(Expression<Func<T, object>> propertyModel)
        {
            var name = ExpressionHelper.GetExpressionText(propertyModel);
            if (string.IsNullOrEmpty(name))
            {
                var body = propertyModel.Body as MemberExpression;
                if (body == null)
                {
                    var ubody = (UnaryExpression)propertyModel.Body;
                    body = (MemberExpression)ubody.Operand;
                    name = body.Member.Name;
                }
            }
            return name;
        }



    }
}