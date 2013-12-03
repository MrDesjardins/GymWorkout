using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Shared;

namespace Model
{
    /// <summary>
    /// Link a property to a validation result.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class EnhancedMappedValidationResult<TEntity> : ValidationResult
    {
        /// <summary>
        /// Property in error on which the validation result error is assigned.
        /// Getter only because we want the exception to be set when the validation result
        /// is created.
        /// </summary>
        /// <value>
        /// The property.
        /// </value>
        public Expression<Func<TEntity, object>> Property { get; private set; }

        /// <summary>
        /// Initializes by setting the propertu and the error.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="errorMessage">The error message.</param>
        public EnhancedMappedValidationResult(Expression<Func<TEntity,object>> property ,string errorMessage): base(errorMessage,new List<string>())
        {
            Property = property;
            ((List<string>)base.MemberNames).Add(LambdaUtilities.GetExpressionText(Property));
        }
    }
}