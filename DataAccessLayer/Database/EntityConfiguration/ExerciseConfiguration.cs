using System.Data.Entity.ModelConfiguration;
using Model;

namespace DataAccessLayer.Database.EntityConfiguration
{
    public class ExerciseConfiguration : EntityTypeConfiguration<Exercise>
    {
        public ExerciseConfiguration()
        {
            this.HasMany(x => x.WorkoutSessionExercices)
                .WithRequired(e => e.Exercise)
                .WillCascadeOnDelete(true);

            this.HasRequired(d => d.Muscle).WithMany(d => d.Exercises);
        }
    }
}