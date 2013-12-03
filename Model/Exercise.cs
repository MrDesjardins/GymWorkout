using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Complex;

namespace Model
{
    public class Exercise : BaseModel
    {
        public LocalizedString Name { get; set; }
        public virtual Muscle Muscle { get; set; }
        public ICollection<WorkoutSessionExercise> WorkoutSessionExercices { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            if (Name==null)
            {
                yield return new EnhancedMappedValidationResult<Exercise>(d=>d.Name,"Name is mandatory");
                yield break;
            }
            
            if (Name.French == null || Name.French.Length < 3)
            {
                yield return new EnhancedMappedValidationResult<Exercise>(d=>d.Name.French,"Exercise's French name must be over 3 characters");
            }

            if (Name.English == null || Name.English.Length < 3)
            {
                yield return new EnhancedMappedValidationResult<Exercise>(d=>d.Name.English,"Exercise's English name must be over 3 characters");
            }

            if (Muscle == null)
            {
                yield return new EnhancedMappedValidationResult<Exercise>(d=>d.Muscle,"Exercice must be assigned to a muscle");
            }
        }

    }
}