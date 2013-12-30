using System.Data.Entity.ModelConfiguration;
using Model;

namespace DataAccessLayer.Database.EntityConfiguration
{
    public class MuscleConfiguration : EntityTypeConfiguration<Muscle>
    {
        public MuscleConfiguration()
        {
            //this.Property(m => m.Timestamp).IsConcurrencyToken().IsConcurrencyToken();
        }
    }
}