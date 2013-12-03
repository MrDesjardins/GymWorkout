using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Definitions;

namespace Model
{
    public abstract class BaseModel : IValidatableObject, IEntity
    {
        public const int NOT_INITIALIZED = -1;
        public int Id { get; set; }

        protected BaseModel()
        {
            Id = NOT_INITIALIZED;
        }

        #region Implementation of IValidatableObject

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        #endregion

    }
}