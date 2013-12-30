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
            //context.Database.Initialize(true);

            var userStore = new UserStore<ApplicationUser>();
            var manager = new UserManager<ApplicationUser>(userStore);

            var role = new IdentityUserRole {Role = new IdentityRole(Model.Roles.ADMINISTRATOR)};
            var user = new ApplicationUser() {UserName = "123123", Email = "123123@123.com", Language = "en-US"};
            user.Roles.Add(role);
            IdentityResult result = manager.Create(user, "123123");

            var role2 = new IdentityUserRole {Role = new IdentityRole(Model.Roles.NORMAL)};
            var user2 = new ApplicationUser() {UserName = "qweqwe", Email = "qweqwe@qweqwe.com", Language = "fr-CA"};
            user.Roles.Add(role2);
            IdentityResult result2 = manager.Create(user2, "qweqwe");
        }

    }
}