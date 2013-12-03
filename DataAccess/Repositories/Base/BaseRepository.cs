using WorkoutPlanner.Database;

namespace WorkoutPlanner.Repositories.Base
{
    public class BaseRepository
    {
        protected IDatabaseContext DatabaseContext { get; private set; }

        protected BaseRepository(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }
    }
}