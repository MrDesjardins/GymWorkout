using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;

namespace DataAccessLayer.Database
{
    public class IdentityContext:IdentityDbContext<ApplicationUser>
    {
       
        public IdentityContext()
        {
            base.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings[DatabaseContext.DEFAULTCONNECTION].ConnectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>();
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
        }
    }

    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            //this.HasKey(d => d.Id);
            //this.Ignore(d => d.UserId);
        }
    }
}
