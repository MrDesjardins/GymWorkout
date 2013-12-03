using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using WorkoutPlanner.Database;
using WorkoutPlanner.Models;
using WorkoutPlanner.Repositories.Base;
using WorkoutPlanner.Repositories.Definitions;

namespace WorkoutPlanner.Repositories.Implementations
{
    public class WorkoutRepository : BaseRepository, IWorkoutRepository
    {
        public WorkoutRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        #region Implementation of IRepository<Workout>

        public IQueryable<Workout> GetAll()
        {
            return DatabaseContext.SetOwnable<Workout>().Include(x => x.Sessions);
        }

        public Workout Get(int id)
        {
            return DatabaseContext.SetOwnable<Workout>().Include(x => x.Sessions).Single(c => c.Id == id);
        }

        public int Insert(Workout entity)
        {
            if (entity.Sessions != null && !entity.Sessions.Any())
            {
                entity.Sessions = null;
            }

            DatabaseContext.SetOwnable<Workout>().Add(entity);
            return DatabaseContext.SaveChanges();
        }

        public int Update(Workout entity)
        {
            Workout fromDatabase = Get(entity.Id);
            DatabaseContext.Entry(fromDatabase).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(fromDatabase).State = EntityState.Modified;

            if (entity.Sessions != null && entity.Sessions.Any())
            {
                foreach (WorkoutSession workoutSession in entity.Sessions)
                {
                    workoutSession.Workout = fromDatabase;

                    if (workoutSession.Id == 0)
                    {
                        DatabaseContext.SetOwnable<WorkoutSession>().Add(workoutSession);
                    }
                    else
                    {
                        if (DatabaseContext.SetOwnable<WorkoutSession>().Local.All(e => e.Id != workoutSession.Id))
                        {
                            DatabaseContext.SetOwnable<WorkoutSession>().Attach(workoutSession);
                        }
                        WorkoutSession fromDatabaseWorkoutSession =
                            DatabaseContext.SetOwnable<WorkoutSession>().Single(x => x.Id == workoutSession.Id);
                        DatabaseContext.Entry(fromDatabaseWorkoutSession).CurrentValues.SetValues(workoutSession);
                        DatabaseContext.Entry(fromDatabaseWorkoutSession).State = EntityState.Modified;
                    }
                }
            }
            /*else
            {
                fromDatabase.Sessions = null;
                IQueryable<WorkoutSession> workoutSessionsToRemove = DatabaseContext.SetOwnable<WorkoutSession>().Where(x => x.Workout.Id == entity.Id);
                foreach (var workoutSessionToRemove in workoutSessionsToRemove)
                {
                    DatabaseContext.SetOwnable<WorkoutSession>().Remove(workoutSessionToRemove);
                }
            }

            if (fromDatabase.Sessions != null)
            {
                foreach (var workoutSessionToRemove in fromDatabase.Sessions.Where(x => entity.Sessions.All(u => u.Id != x.Id)).ToList())
                {
                    DatabaseContext.SetOwnable<WorkoutSession>().Remove(workoutSessionToRemove);
                }
            }*/

            return DatabaseContext.SaveChanges();
        }

        public int Delete(Workout entity)
        {
            DatabaseContext.SetOwnable<Workout>().Remove(entity);
            return DatabaseContext.SaveChanges();
        }

        public int GetAmountWorkoutForCurrentMonth()
        {
            return DatabaseContext.SetOwnable<Workout>().Count(w => w.StartTime.Month == DateTime.Now.Month && w.StartTime.Year == DateTime.Now.Year);
        }
        #endregion
    }
}