using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security;
using BusinessLogic;
using Model;
using Model.Complex;
using Model.Definitions;
using Shared;
using WebMatrix.WebData;
using WorkoutPlanner.Database;
using System.Data.Entity.Core;

namespace DataAccessLayer.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public const string DEFAULTCONNECTION = "DefaultConnection";
        public DatabaseContext(IUserProvider userProvider)
        {
            UserProvider = userProvider;

            base.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings[DEFAULTCONNECTION].ConnectionString;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ValidateOnSaveEnabled = true;
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
                throw new SecurityException("You cannot by-pass the ownable security");
               
            }
            return base.Set<TEntity>();
            
        }


        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            using (this.RemoveValidation())
            {
                var toDelete = this.Set<TEntity>().Find(entity.Id);
                this.Entry(toDelete).State = EntityState.Deleted;
                SaveChanges();
            }
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity is IUserOwnable)
                throw new Exception("Must user UpdateOwnable");

            var entityLocal = Set<TEntity>().Local.SingleOrDefault(d => d.Id == entity.Id);
            if (entityLocal == null)
            {
                entityLocal = this.Set<TEntity>().Attach(entity);
            }
            
            this.Entry(entityLocal).State = EntityState.Modified;
            //this.ChangeTracker.Entries<TEntity>().Single(d => d.Entity == entityLocal).State = EntityState.Modified;
            return entityLocal;
        }

        public TEntity UpdateOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable
        {
            //THIS IS IN COMMENT BECAUSE TO BE SURE WE UPDATE THE USER ENTITY, WE NEED TO GET IT BACK FROM DATABASE
            //ENTITY FRAMEWORK 5.0 DOESNT LET ADD A WHERE CLAUSE TO UPDATE (WHERE UseriD = xxx)
            //var entityLocal = SetOwnable<TEntity>().Local.SingleOrDefault(d => d.Id == entity.Id);
            //if (entityLocal==null)
            //{
            //    entityLocal = this.SetOwnable<TEntity>().Attach(entity);
            //}
            //this.ChangeTracker.Entries<TEntity>().Single(d => d.Entity == entityLocal).State = EntityState.Modified;
            TEntity entityLocal;
            try
            {
                entityLocal = SetOwnable<TEntity>().Single(d => d.Id == entity.Id);
            }
            catch (InvalidOperationException ioe)
            {
                throw new DataNotFoundException(ioe);
            }
            this.Entry(entityLocal).State = EntityState.Modified;
            return entityLocal;
            //this.ChangeTracker.Entries<TEntity>().Single(d => d.Entity == entityLocal).State = EntityState.Modified;
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if (entity is IUserOwnable)
                throw new Exception("Must user InsertOwnable");

            return this.Set<TEntity>().Add(entity);
        }
        public TEntity InsertOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable
        {
            return this.SetOwnable<TEntity>().Add(entity);
        }

        public TEntity Attach<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            if(entity is IUserOwnable)
                throw new Exception("Must user AttachOwnable");

            if (entity != null)
            {
                if (Set<TEntity>().Local.All(e => e.Id != entity.Id))
                {
                    return Set<TEntity>().Attach(entity);
                }
                else
                {
                    return Set<TEntity>().Single(e => e.Id == entity.Id);
                }
            }
            return null;
        }
        public TEntity AttachOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable
        {
            if (entity != null)
            {
                if (SetOwnable<TEntity>().Local.All(e => e.Id != entity.Id))
                {
                    return SetOwnable<TEntity>().Attach(entity);
                }
                else
                {
                    return SetOwnable<TEntity>().Single(e => e.Id == entity.Id);
                }
            }
            return null;
        }

        /// <summary>
        /// Removes the validation that Entity Framework does. This can be used in the case that
        /// we are having attached entity not fully loaded.
        /// </summary>
        /// <returns></returns>
        public IRemoveValidation RemoveValidation()
        {
            return new RemoveValidation(this.Configuration);
        }

        public int SaveChangesWithoutValidation()
        {
            var result = -1;
            this.Configuration.ValidateOnSaveEnabled = false;
            try
            {
                result = this.SaveChanges();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Configuration.ValidateOnSaveEnabled = true;
            }
            return result;
        }

        public IDbSet<TEntity> SetOwnable<TEntity>() where TEntity : class, IUserOwnable
        {
            return new FilteredDbSet<TEntity>(this, entity => entity.UserId == CurrentUser.UserId, entity => entity.UserId = CurrentUser.UserId);
        }

        public void InitializeDatabase()
        {
            WebSecurity.InitializeDatabaseConnection(DEFAULTCONNECTION, "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }/*
        public DbEntityEntry<TEntity> ChangeTracker<TEntity>(TEntity entity) where TEntity : class
        {
             return base.ChangeTracker.Entries<TEntity>().Single(p => p.Entity == entity);
        }*/

        public override int SaveChanges()
        {
            IEnumerable<DbEntityValidationResult> errors = new List<DbEntityValidationResult>();
            if (this.Configuration.ValidateOnSaveEnabled)
            {
                errors = this.GetValidationErrors();
            }
            if (!errors.Any())
            {
                try
                {
                    //base.ChangeTracker.DetectChanges();
                    return base.SaveChanges();
                }
                catch (OptimisticConcurrencyException concurrencyException)
                {
                    throw new DatabaseConcurrencyException("Someone else has edited the entity in the same time of you. Please refresh and save again.", concurrencyException);
                }
                catch (DBConcurrencyException concurrencyException)
                {
                    throw new DatabaseConcurrencyException("Someone else has edited the entity in the same time of you. Please refresh and save again.", concurrencyException);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw new DatabaseConcurrencyException("Someone else has edited the entity in the same time of you. Please refresh and save again.", e);
                }
                catch (Exception)
                {
                    throw;
                }

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

            modelBuilder.ComplexType<LocalizedString>().Ignore(d => d.Current);

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

        public void PrepareScalarValuesForUpdate<TEntity>(TEntity entity) where TEntity : class
        {
           
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
        public WorkoutConfiguration()
        {
            base.HasMany(d => d.Sessions)
                .WithRequired(d=>d.Workout)
                .WillCascadeOnDelete(true);
        }
    }


    public class WorkoutSessionConfiguration : EntityTypeConfiguration<WorkoutSession>
    {
        public WorkoutSessionConfiguration()
        {
            //this.HasMany(x => x.WorkoutSessionExercises)
            //    .WithRequired(e => e.WorkoutSession)
            //    .WillCascadeOnDelete(true);
        }
    }

    public class WorkoutSessionExerciseConfiguration : EntityTypeConfiguration<WorkoutSessionExercise>
    {
        public WorkoutSessionExerciseConfiguration()
        {
            this.Ignore(x => x.RestBetweenExercices);
            this.HasRequired(e=>e.WorkoutSession)
                .WithMany(e=>e.WorkoutSessionExercises)
                .WillCascadeOnDelete(true);
        }
    }

    public class ExerciseConfiguration : EntityTypeConfiguration<Exercise>
    {
        public ExerciseConfiguration()
        {
            this.HasMany(x => x.WorkoutSessionExercices)
            .WithRequired(e => e.Exercise)
            .WillCascadeOnDelete(true);

            this.HasRequired(d => d.Muscle).WithMany(d => d.Exercises);
        }
    }

    public class MuscleConfiguration : EntityTypeConfiguration<Muscle>
    {
        public MuscleConfiguration()
        {
            //this.Property(m => m.Timestamp).IsConcurrencyToken().IsConcurrencyToken();
        }
    }

    public class MuscleGroupConfiguration : EntityTypeConfiguration<MuscleGroup>
    {
    }
}