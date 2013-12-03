using System.Data;
using System.Data.Entity;
using System.Linq;
using WorkoutPlanner.Database;
using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;
using WorkoutPlanner.Repositories.Definitions;

namespace WorkoutPlanner.Repositories.Implementations
{
    public class WorkoutSessionRepository : BaseRepository, IWorkoutSessionRepository
    {
        public WorkoutSessionRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        #region Implementation of IRepository<Workout>

        public IQueryable<WorkoutSession> GetAll()
        {
            return DatabaseContext.SetOwnable<WorkoutSession>();
        }

        public WorkoutSession Get(int id)
        {
            //return DatabaseContext.SetOwnable<WorkoutSession>().Include(x=>x.WorkoutSessionExercises).Single(c => c.Id == id);
            /*
             var query = from workoutSession in DatabaseContext.SetOwnable<WorkoutSession>()
                        from workoutSessionExercises in workoutSession.WorkoutSessionExercises
                        from exerciseInfo in workoutSessionExercises.Exercise
                        select workoutSession;
             */
            /*var query = DatabaseContext.SetOwnable<WorkoutSession>()
                                        .Include(x => x.WorkoutSessionExercises.SelectMany(e=>e.Exercise))
                                        .Where(e => e.Id == id);*/
            var query = DatabaseContext.SetOwnable<WorkoutSession>()
                .Include(d=>d.Workout)
                .Include("WorkoutSessionExercises.Exercise")
                .Where(w => w.Id == id);
            var first= query.First(c=>c.Id==id);
            return first;
        }

        public int Insert(WorkoutSession entity)
        {
            if (entity.Workout != null)
            {
                if (DatabaseContext.SetOwnable<Workout>().Local.All(e => e.Id != entity.Workout.Id))
                {
                    DatabaseContext.SetOwnable<Workout>().Attach(entity.Workout);
                }
            }
            
            if (entity.WorkoutSessionExercises != null && !entity.WorkoutSessionExercises.Any())
            {
                entity.WorkoutSessionExercises = null;
            }

            DatabaseContext.SetOwnable<WorkoutSession>().Add(entity);
            return DatabaseContext.SaveChanges();
        }

        public int Update(WorkoutSession entity)
        {
            WorkoutSession fromDatabase = Get(entity.Id);
            DatabaseContext.Entry(fromDatabase).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(fromDatabase).State = EntityState.Modified;

            return DatabaseContext.SaveChanges();
        }

        public int Delete(WorkoutSession entity)
        {
            DatabaseContext.SetOwnable<WorkoutSession>().Remove(entity);
            return DatabaseContext.SaveChanges();
        }

        public IQueryable<WorkoutSession> GetAllForkWorkout(int workoutId)
        {
            return DatabaseContext.SetOwnable<WorkoutSession>().Where(w => w.Workout.Id == workoutId);
        }

        #endregion
    }
}