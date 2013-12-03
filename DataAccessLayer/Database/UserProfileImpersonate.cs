using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using BusinessLogic;
using Model.Definitions;

namespace DataAccessLayer.Database
{
    public class UserProfileImpersonate : IDatabaseContext, IDisposable
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IUserProvider _oldUserProvider;

        #region Implementation of IDisposable

        public UserProfileImpersonate(DatabaseContext dbContext, ICurrentUser userProfile)
        {
            _databaseContext = dbContext;
            _oldUserProvider = dbContext.UserProvider;
            _databaseContext.UserProvider = new ImpersonateUserProvider(userProfile);
        }

        public void Dispose()
        {
            _databaseContext.UserProvider = _oldUserProvider;
        }

        #endregion

        #region Implementation of IDatabaseContext

        public int SaveChanges()
        {
            return _databaseContext.SaveChanges();
        }

        public IDbSet<TEntity> SetOwnable<TEntity>() where TEntity : class, IUserOwnable
        {
            return _databaseContext.SetOwnable<TEntity>();
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _databaseContext.Set<TEntity>();
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return _databaseContext.Entry(entity);
        }

        public void InitializeDatabase()
        {
            _databaseContext.InitializeDatabase();
        }

        public UserProfileImpersonate Impersonate(ICurrentUser userProfile)
        {
            return _databaseContext.Impersonate(userProfile);
        }

        public DbChangeTracker ChangeTracker { get {
            return _databaseContext.ChangeTracker;
            }
        }

     

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _databaseContext.Configuration.ValidateOnSaveEnabled = false;
            if (!this.Set<TEntity>().Local.Any(d => d.Id == entity.Id))
            {
                this.Set<TEntity>().Attach(entity);
            }
            this.Set<TEntity>().Remove(entity);
            SaveChanges();
            _databaseContext.Configuration.ValidateOnSaveEnabled = true;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
           return _databaseContext.Update(entity);
        }

        public TEntity UpdateOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable
        {
            return _databaseContext.UpdateOwnable(entity);
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            return _databaseContext.Insert(entity);
        }

        public TEntity InsertOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable
        {
            return _databaseContext.InsertOwnable(entity);
        }

        public TEntity Attach<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            return _databaseContext.Attach(entity);
        }

        public TEntity AttachOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable
        {
            return _databaseContext.AttachOwnable(entity);
        }

        public IRemoveValidation RemoveValidation()
        {
            return _databaseContext.RemoveValidation();
        }

        public int SaveChangesWithoutValidation()
        {
            return _databaseContext.SaveChangesWithoutValidation();
        }

        #endregion
    }
}