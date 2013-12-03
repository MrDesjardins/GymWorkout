using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WorkoutPlanner.BusinessLogic;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.Definitions;

namespace WorkoutPlanner.Database
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

        #endregion
    }
}