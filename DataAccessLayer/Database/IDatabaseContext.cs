using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Model;
using Model.Definitions;
using WorkoutPlanner.Database;

namespace DataAccessLayer.Database
{
    public interface IDatabaseContext
    {
        int SaveChanges();
        IDbSet<TEntity> SetOwnable<TEntity>() where TEntity : class, IUserOwnable;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void InitializeDatabase();
        UserProfileImpersonate Impersonate(ICurrentUser userProfile);
        DbChangeTracker ChangeTracker { get; }
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
        TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        TEntity UpdateOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable;
        TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity;
        TEntity InsertOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable;
        TEntity Attach<TEntity>(TEntity entity) where TEntity : class, IEntity;
        TEntity AttachOwnable<TEntity>(TEntity entity) where TEntity : class, IEntity, IUserOwnable;
        IRemoveValidation RemoveValidation();
        int SaveChangesWithoutValidation();
    }

}