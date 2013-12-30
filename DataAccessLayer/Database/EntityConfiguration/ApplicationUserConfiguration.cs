using System.Data.Entity.ModelConfiguration;
using Model;

namespace DataAccessLayer.Database.EntityConfiguration
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            this.HasKey(d => d.Id);
            this.Ignore(d => d.UserId);
        }
    }
}