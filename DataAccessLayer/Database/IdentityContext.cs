using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Database.EntityConfiguration;
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
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
        }
    }
}
