using System.Data.Entity.ModelConfiguration;
using Model;

namespace DataAccessLayer.Database.EntityConfiguration
{
    public class WorkoutSessionConfiguration : EntityTypeConfiguration<WorkoutSession>
    {
        public WorkoutSessionConfiguration()
        {
            //this.HasMany(x => x.WorkoutSessionExercises)
            //    .WithRequired(e => e.WorkoutSession)
            //    .WillCascadeOnDelete(true);
        }
    }
}