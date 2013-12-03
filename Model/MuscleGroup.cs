using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Complex;

namespace Model
{
    public class MuscleGroup : BaseModel
    {
        public LocalizedString Name { get; set; }
        public ICollection<Muscle> Muscles { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == null)
            {
                yield return new ValidationResult("Name is mandatory", new[] { "Name" });
                yield break;
            }

            if (Name.French == null || Name.French.Length < 3)
            {
                yield return new ValidationResult("French name must be over 3 characters");
            }

            if (Name.English == null || Name.English.Length < 3)
            {
                yield return new ValidationResult("English name must be over 3 characters");
            }
        }
    }
}