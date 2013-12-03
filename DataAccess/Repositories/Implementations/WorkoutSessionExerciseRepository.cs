using System.Data;
using System.Data.Entity;
using System.Linq;
using WorkoutPlanner.Database;
using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;
using WorkoutPlanner.Repositories.Definitions;

namespace WorkoutPlanner.Repositories.Implementations
{
    public class WorkoutSessionExerciseRepository : BaseRepository, IWorkoutSessionExerciseRepository
    {
        public WorkoutSessionExerciseRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        #region Implementation of IRepository

        public IQueryable<WorkoutSessionExercise> GetAll()
        {
            return DatabaseContext.SetOwnable<WorkoutSessionExercise>();
        }

        public WorkoutSessionExercise Get(int id)
        {
            return DatabaseContext.SetOwnable<WorkoutSessionExercise>().Single(c => c.Id == id);
        }

        public int Insert(WorkoutSessionExercise entity)
        {
            if (entity.Exercise != null)
            {
                if (DatabaseContext.Set<Exercise>().Local.All(e => e.Id != entity.Exercise.Id))
                {
                    DatabaseContext.Set<Exercise>().Attach(entity.Exercise);
                }
            }

            if (entity.WorkoutSession != null)
            {
                if (DatabaseContext.SetOwnable<WorkoutSession>().Local.All(e => e.Id != entity.WorkoutSession.Id))
                {
                    DatabaseContext.SetOwnable<WorkoutSession>().Attach(entity.WorkoutSession);
                }
            }
            DatabaseContext.SetOwnable<WorkoutSessionExercise>().Add(entity);
            return DatabaseContext.SaveChanges();
        }

        public int Update(WorkoutSessionExercise entity)
        {
            WorkoutSessionExercise fromDatabase = Get(entity.Id);
            DatabaseContext.Entry(fromDatabase).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(fromDatabase).State = EntityState.Modified;

            return DatabaseContext.SaveChanges();
        }

        public int Delete(WorkoutSessionExercise entity)
        {
            DatabaseContext.SetOwnable<WorkoutSessionExercise>().Remove(entity);
            return DatabaseContext.SaveChanges();
        }

        public IQueryable<WorkoutSessionExercise> GetForWorkoutSession(int workoutSessionId)
        {
            return DatabaseContext.SetOwnable<WorkoutSessionExercise>()
                    .Where(w => w.WorkoutSession.Id  == workoutSessionId);
        }

        #endregion
    }
}