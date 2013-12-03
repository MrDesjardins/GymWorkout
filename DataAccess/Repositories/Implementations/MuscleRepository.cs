using System.Data;
using System.Linq;
using WorkoutPlanner.Database;
using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;
using WorkoutPlanner.Repositories.Definitions;

namespace WorkoutPlanner.Repositories.Implementations
{
    public class MuscleRepository : BaseRepository, IMuscleRepository
    {
        public MuscleRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public IQueryable<Muscle> GetAll()
        {
            return DatabaseContext.Set<Muscle>();
        }

        public Muscle Get(int id)
        {
            return DatabaseContext.Set<Muscle>().Single(x => x.Id == id);
        }

        public int Insert(Muscle entity)
        {
            DatabaseContext.Set<Muscle>().Add(entity);
            return DatabaseContext.SaveChanges();   
        }

        public int Update(Muscle entity)
        {
            Muscle fromDatabase = Get(entity.Id);
            DatabaseContext.Entry(fromDatabase).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(fromDatabase).State = EntityState.Modified;

            return DatabaseContext.SaveChanges();
        }

        public int Delete(Muscle entity)
        {
            DatabaseContext.Set<Muscle>().Remove(entity);
            return DatabaseContext.SaveChanges();   
        }
    }
}