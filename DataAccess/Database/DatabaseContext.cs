using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security;
using WebMatrix.WebData;
using WorkoutPlanner.BusinessLogic;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.Definitions;

namespace WorkoutPlanner.Database
{
    public static class DatabaseContextExtension
    {

        public static void AddRange<TEntity>(this DbSet<TEntity> dbset, IEnumerable<TEntity> list) where TEntity : class
        {
            foreach (var item in list)
            {
                dbset.Add(item);
            }

        }



    }

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public const string DEFAULTCONNECTION = "DefaultConnection";
        public DatabaseContext(IUserProvider userProvider)
        {
            UserProvider = userProvider;

            base.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings[DEFAULTCONNECTION].ConnectionString;
            Configuration.ProxyCreationEnabled = false;
        }

        public IUserProvider UserProvider { get; set; }

        public ICurrentUser CurrentUser
        {
            get { return UserProvider.Account; }
        }

        #region Implementation of IDatabaseContext


        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            if (typeof(IUserOwnable) is TEntity)
            {
                throw new SecurityException("You cannot by pass the ownable security");
            }
            return base.Set<TEntity>();
        }


        public IDbSet<TEntity> SetOwnable<TEntity>() where TEntity : class, IUserOwnable
        {
            return new FilteredDbSet<TEntity>(this, entity => entity.UserId == CurrentUser.UserId, entity => entity.UserId = CurrentUser.UserId);
        }

        public void InitializeDatabase()
        {
            WebSecurity.InitializeDatabaseConnection(DEFAULTCONNECTION, "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
        
        public override int SaveChanges()
        {
            var errors = this.GetValidationErrors();
            if (!errors.Any())
            {
                return base.SaveChanges();
            }
            else
            {
                /*var x = string.Concat(errors
                    .SelectMany(e => e.ValidationErrors
                    .Select(d => d.PropertyName + ":" + d.ErrorMessage + ", ")))
                    ;
                throw new Exception(x);*/
                throw new DatabaseValidationErrors(errors);
            }
            
        }
        
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Configurations.Add(new WorkoutConfiguration());
            modelBuilder.Configurations.Add(new WorkoutSessionConfiguration());
            modelBuilder.Configurations.Add(new WorkoutSessionExerciseConfiguration());
            modelBuilder.Configurations.Add(new ExerciseConfiguration());
            modelBuilder.Configurations.Add(new MuscleConfiguration());
            modelBuilder.Configurations.Add(new MuscleGroupConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
        }

        public UserProfileImpersonate Impersonate(ICurrentUser userProfile)
        {
            return new UserProfileImpersonate(this, userProfile);
        }
    }

    public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            this.Property(d => d.Email).HasColumnName("Email");
            this.Property(d => d.UserName).HasColumnName("UserName");
        }
    }


    public class WorkoutConfiguration : EntityTypeConfiguration<Workout>
    {
    }


    public class WorkoutSessionConfiguration : EntityTypeConfiguration<WorkoutSession>
    {
        public WorkoutSessionConfiguration()
        {
            //this.HasMany(x => x.WorkoutSessionExercises).WithRequired(e => e.WorkoutSession);
        }
    }

    public class WorkoutSessionExerciseConfiguration : EntityTypeConfiguration<WorkoutSessionExercise>
    {
        public WorkoutSessionExerciseConfiguration()
        {
            this.Ignore(x => x.RestBetweenExercices);
        }
    }

    public class ExerciseConfiguration : EntityTypeConfiguration<Exercise>
    {
    }

    public class MuscleConfiguration : EntityTypeConfiguration<Muscle>
    {
    }

    public class MuscleGroupConfiguration : EntityTypeConfiguration<MuscleGroup>
    {
    }
}