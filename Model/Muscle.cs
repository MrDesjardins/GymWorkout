using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Complex;


namespace Model
{
    public class Muscle : BaseModel, IConcurrencyProtection
    {
        public LocalizedString Name { get; set; }
        public virtual MuscleGroup Group { get; set; }
        public ICollection<Exercise> Exercises { get; set; }

       

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == null)
            {
                yield return new EnhancedMappedValidationResult<Muscle>(d => d.Name, "Name is mandatort");
                yield break;
            }

            if (Name.French != null && Name.French.Length < 3)
            {
                yield return new EnhancedMappedValidationResult<Muscle>(d=>d.Name.French,"Muscle's French name must be over 3 characters");
            }

            if (Name.English != null && Name.English.Length < 3)
            {
                yield return new EnhancedMappedValidationResult<Muscle>(d => d.Name.English, "Muscle's English name must be over 3 characters");
            }

            if (Name.English == null && Name.French == null)
            {
                yield return new ValidationResult("Muscle name must be specified");
            }
        }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }


}