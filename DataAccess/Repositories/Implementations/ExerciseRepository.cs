using System.Data;
using System.Data.Entity;
using System.Linq;
using WorkoutPlanner.Database;
using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;
using WorkoutPlanner.Repositories.Definitions;

namespace WorkoutPlanner.Repositories.Implementations
{
    public class ExerciseRepository : BaseRepository, IExerciseRepository
    {
        public ExerciseRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        public Exercise Get(int id)
        {
            return DatabaseContext.Set<Exercise>().Include(d=>d.Muscle).Single(x => x.Id == id);
        }

        public int Insert(Exercise entity)
        {

            if (entity.Muscle != null)
            {
                if (DatabaseContext.Set<Muscle>().Local.All(e => e.Id != entity.Muscle.Id))
                {
                    DatabaseContext.Set<Muscle>().Attach(entity.Muscle);
                }
            }
            DatabaseContext.Set<Exercise>().Add(entity);
            return DatabaseContext.SaveChanges();   
        }

        public int Update(Exercise entity)
        {
            Exercise fromDatabase = Get(entity.Id);
            DatabaseContext.Entry(fromDatabase).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(fromDatabase).State = EntityState.Modified;

            return DatabaseContext.SaveChanges();
        }

        public int Delete(Exercise entity)
        {
            DatabaseContext.Set<Exercise>().Remove(entity);
            return DatabaseContext.SaveChanges();
        }

        public IQueryable<Exercise>GetAll()
        {
            return DatabaseContext.Set<Exercise>();
        }

    }
}