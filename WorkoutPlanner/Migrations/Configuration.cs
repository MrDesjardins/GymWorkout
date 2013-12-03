using System.Collections.Generic;
using DataAccessLayer.Database;
using Model;
using Model.Complex;
using WebMatrix.WebData;
using Roles = System.Web.Security.Roles;
using WorkoutPlanner.Database;
using System.Data.Entity.Migrations;

namespace WorkoutPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);


            WebSecurity.InitializeDatabaseConnection(
            "DefaultConnection",
            "UserProfile",
            "UserId",
            "UserName", autoCreateTables: true);

            if (!Roles.RoleExists(Model.Roles.ADMINISTRATOR))
                Roles.CreateRole(Model.Roles.ADMINISTRATOR);

            if (!Roles.RoleExists(Model.Roles.NORMAL))
                Roles.CreateRole(Model.Roles.NORMAL);

            if (!WebSecurity.UserExists("123123"))
                WebSecurity.CreateUserAndAccount("123123", "123123", new { Email="123123@123.com", Language="fr-CA"});
            if (!WebSecurity.UserExists("qweqwe"))
                WebSecurity.CreateUserAndAccount("qweqwe", "qweqwe", new { Email = "qweqwe@qwe.com", Language = "en-US" });

            if (!((IList<string>)Roles.GetRolesForUser("123123")).Contains(Model.Roles.ADMINISTRATOR))
                Roles.AddUsersToRoles(new[] { "123123", "qweqwe" }, new[] { Model.Roles.ADMINISTRATOR });
            if (!((IList<string>)Roles.GetRolesForUser("qweqwe")).Contains(Model.Roles.NORMAL))
                Roles.AddUsersToRoles(new[] { "qweqwe" }, new[] { Model.Roles.NORMAL });


            context.Database.Initialize(true);

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
            

            context.Set<Exercise>().AddOrUpdate(new Exercise { Id = 1, Name = new LocalizedString { French = "Pompe", English = "Push up"},Muscle = muscles.Single(d=>d.Id==3)});
            context.Set<Exercise>().AddOrUpdate(new Exercise { Id = 2, Name = new LocalizedString { French = "Pectoraux sur banc plat avec barre", English = "Flat barbell bench press" }, Muscle = muscles.Single(d => d.Id == 10) });
            context.Set<Exercise>().AddOrUpdate(new Exercise { Id = 3, Name = new LocalizedString { French = "Redressement assi", English = "Setup" }, Muscle = muscles.Single(d => d.Id == 6) });
            context.Set<Exercise>().AddOrUpdate(new Exercise { Id = 4, Name = new LocalizedString { French = "Bike", English = "Vélo stationnaire" }, Muscle = muscles.Single(d => d.Id == 8) });

            using (var db = context.Impersonate(new UserProfile { UserId = 1 }))
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
            using (var db = context.Impersonate(new UserProfile { UserId = 2 }))
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

        }

 
    }
}
