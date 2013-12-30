using System.Data.Entity.ModelConfiguration;
using Model;

namespace DataAccessLayer.Database.EntityConfiguration
{
    public class WorkoutSessionExerciseConfiguration : EntityTypeConfiguration<WorkoutSessionExercise>
    {
        public WorkoutSessionExerciseConfiguration()
        {
            this.Ignore(x => x.RestBetweenExercices);
            this.HasRequired(e=>e.WorkoutSession)
                .WithMany(e=>e.WorkoutSessionExercises)
                .WillCascadeOnDelete(true);
        }
    }
}