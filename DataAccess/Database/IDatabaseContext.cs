using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WorkoutPlanner.BusinessLogic;
using WorkoutPlanner.Models;
using WorkoutPlanner.Models.Definitions;

namespace WorkoutPlanner.Database
{
    public interface IDatabaseContext   
    {
        int SaveChanges();
        IDbSet<TEntity> SetOwnable<TEntity>() where TEntity : class, IUserOwnable;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void InitializeDatabase();
        UserProfileImpersonate Impersonate(ICurrentUser userProfile);
    }
}