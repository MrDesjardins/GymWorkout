using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using DataAccessLayer.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using Model.Complex;
using Roles = System.Web.Security.Roles;
using WorkoutPlanner.Database;
using System.Data.Entity.Migrations;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace WorkoutPlanner.Migrations
{

    //PM> update-database -ConfigurationTypeName "Configuration" -verbose
    public class Configuration : DbMigrationsConfiguration<DatabaseContext>//<DatabaseContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(userStore);

            var role = new IdentityUserRole { Role = new IdentityRole(Model.Roles.ADMINISTRATOR) };
            var user = new ApplicationUser() { UserName = "123123", Email = "123123@123.com", Language = "en-US" };
            user.Roles.Add(role);
            IdentityResult result = manager.Create(user, "123123");

            var role2 = new IdentityUserRole { Role = new IdentityRole(Model.Roles.NORMAL) };
            var user2 = new ApplicationUser() { UserName = "qweqwe", Email = "qweqwe@qweqwe.com", Language = "fr-CA" };
            user.Roles.Add(role2);
            IdentityResult result2 = manager.Create(user2, "qweqwe");

            var muscles = new[]{new Muscle { Id = 1, Name = new LocalizedString { French = "Cou", English = "Neck" } },
                                new Muscle { Id = 2, Name = new LocalizedString { French = "Épaule", English = "Shoulder" } },
                                new Muscle { Id = 3, Name = new LocalizedString { French = "Haut du bras", English = "Upper Arms" } },
                                new Muscle { Id = 4, Name = new LocalizedString { French = "Bas du bras", English = "Forearms" } },
                                new Muscle { Id = 5, Name = new LocalizedString { French = "Dos", English = "Back" } },
                                new Muscle { Id = 6, Name = new LocalizedString { French = "Tronc", English = "Waist" } },
                                new Muscle { Id = 7, Name = new LocalizedString { French = "Hanche", English = "Hips" } },
                                new Muscle { Id = 8, Name = new LocalizedString { French = "Jambes", English = "Thighs" } },
                                new Muscle { Id = 9, Name = new LocalizedString { French = "Mollets", English = "Calves" } },
                                new Muscle { Id = 10, Name = new LocalizedString { French = "Pectoraux", English = "Chest" } }
                                };


            context.Set<Muscle>().AddOrUpdateRange(muscles);


            context.Set<Exercise>().AddOrUpdate(new Exercise { Id = 1, Name = new LocalizedString { French = "Pompe", English = "Push up" }, Muscle = muscles.Single(d => d.Id == 3) });
            context.Set<Exercise>().AddOrUpdate(new Exercise { Id = 2, Name = new LocalizedString { French = "Pectoraux sur banc plat avec barre", English = "Flat barbell bench press" }, Muscle = muscles.Single(d => d.Id == 10) });
            context.Set<Exercise>().AddOrUpdate(new Exercise { Id = 3, Name = new LocalizedString { French = "Redressement assi", English = "Setup" }, Muscle = muscles.Single(d => d.Id == 6) });
            context.Set<Exercise>().AddOrUpdate(new Exercise { Id = 4, Name = new LocalizedString { French = "Bike", English = "Vélo stationnaire" }, Muscle = muscles.Single(d => d.Id == 8) });

            using (var db = context.Impersonate(new ApplicationUser() { UserId = "1" }))
            {
                var workout1 = new Workout { Id = 1, Name = "My First workout user1", StartTime = DateTime.Now.Add(TimeSpan.FromDays(-10)), Goal = "Increase body mass" };
                var workout2 = new Workout { Id = 2, Name = "My Second workout user1", StartTime = DateTime.Now, Goal = "Increase chest muscle, lower fat around abs" };
                db.SetOwnable<Workout>().AddOrUpdate(workout1);
                db.SetOwnable<Workout>().AddOrUpdate(workout2);
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 1, Name = "Monday", Workout = workout1 });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 2, Name = "Thusday", Workout = workout1, });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 3, Name = "Wednesday", Workout = workout1, });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 4, Name = "Thursday", Workout = workout1, });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 5, Name = "Friday", Workout = workout1, });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 6, Name = "Saterday", Workout = workout1, });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 7, Name = "Day1", Workout = workout2, });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 8, Name = "Day2", Workout = workout2, });

            }
            using (var db = context.Impersonate(new ApplicationUser { UserId = "2" }))
            {
                var workout3 = new Workout { Id = 3, Name = "My First workout user2", StartTime = DateTime.Now.Add(TimeSpan.FromDays(-10)), Goal = "Increase body mass" };
                var workout4 = new Workout { Id = 4, Name = "My Second workout user2", StartTime = DateTime.Now, Goal = "Increase chest muscle, lower fat around abs" };
                db.SetOwnable<Workout>().AddOrUpdate(workout3);
                db.SetOwnable<Workout>().AddOrUpdate(workout4);
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 9, Name = "Training Arm", Workout = workout3 });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 10, Name = "Training Leg", Workout = workout3, });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 11, Name = "Cardio Training", Workout = workout4, });
                db.SetOwnable<WorkoutSession>().AddOrUpdate(new WorkoutSession { Id = 12, Name = "Muscle Training", Workout = workout4, });
            }

            base.Seed(context);
        }


    }
}
