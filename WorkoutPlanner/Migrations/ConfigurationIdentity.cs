using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using DataAccessLayer.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;

namespace WorkoutPlanner.Migrations
{
    internal sealed class ConfigurationIdentity: DbMigrationsConfiguration<IdentityContext>
    {
        public ConfigurationIdentity()
        {
            AutomaticMigrationsEnabled = true;
        }


        protected override void Seed(IdentityContext context)
        {
            base.Seed(context);

        }

    }
}