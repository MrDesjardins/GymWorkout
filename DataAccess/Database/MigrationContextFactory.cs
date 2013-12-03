using System.Data.Entity.Infrastructure;
using WorkoutPlanner.BusinessLogic;

namespace WorkoutPlanner.Database
{
    public class MigrationContextFactory : IDbContextFactory<DatabaseContext>
    {
        #region Implementation of IDbContextFactory<out DatabaseContext>

        public DatabaseContext Create()
        {
            return new DatabaseContext(new WebUserProvider());
        }

        #endregion
    }
}