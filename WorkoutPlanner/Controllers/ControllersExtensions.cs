using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Validations;
using WorkoutPlanner.Database;
using WorkoutPlanner.Validations;

namespace WorkoutPlanner.Controllers
{
    public static class ControllersExtensions
    {
        /// <summary>
        /// Add to the model state a list of error that came from properties. If the property name
        /// is empty, this one will be without property (general)
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="propertyErrors">The property errors.</param>
        public static void AddValidationErrors(this ModelStateDictionary modelState, IValidationErrors propertyErrors)
        {
            foreach (var databaseValidationError in propertyErrors.Errors)
            {
                modelState.AddModelError(databaseValidationError.PropertyName??string.Empty, databaseValidationError.PropertyExceptionMessage);
            }
        }
    }
}