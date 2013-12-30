using System.Data.Entity.ModelConfiguration;
using Model;

namespace DataAccessLayer.Database.EntityConfiguration
{
    public class WorkoutConfiguration : EntityTypeConfiguration<Workout>
    {
        public WorkoutConfiguration()
        {
            base.HasMany(d => d.Sessions)
                .WithRequired(d=>d.Workout)
                .WillCascadeOnDelete(true);
        }
    }
}