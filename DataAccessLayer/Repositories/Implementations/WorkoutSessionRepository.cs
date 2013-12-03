using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Base;
using DataAccessLayer.Repositories.Definitions;
using Model;

namespace DataAccessLayer.Repositories.Implementations
{
    public class WorkoutSessionRepository : BaseRepository<WorkoutSession>, IWorkoutSessionRepository
    {
        public WorkoutSessionRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        #region Implementation of IRepository<Workout>

        public override IQueryable<WorkoutSession> GetAll()
        {
            return DatabaseContext.SetOwnable<WorkoutSession>();
        }

        public override WorkoutSession Get(int id)
        {
            var query = DatabaseContext.SetOwnable<WorkoutSession>()
                .Include(d=>d.Workout)
                .Include(d=>d.WorkoutSessionExercises.Select(e=>e.Exercise))
                .Where(w => w.Id == id);
            var first= query.First(c=>c.Id==id);
            return first;
        }

        public override int Insert(WorkoutSession entity)
        {
            if (entity.WorkoutSessionExercises != null && !entity.WorkoutSessionExercises.Any())
            {
                entity.WorkoutSessionExercises = null;
            }
            DatabaseContext.AttachOwnable(entity.Workout);
            DatabaseContext.InsertOwnable(entity);
            return DatabaseContext.SaveChanges();
        }

        public override int Update(WorkoutSession entity)
        {
            DatabaseContext.UpdateOwnable(entity);
            return DatabaseContext.SaveChanges();
        }

        public override int Delete(WorkoutSession entity)
        {
            DatabaseContext.Delete(entity);
            return DatabaseContext.SaveChanges();
        }

        public override IQueryable<WorkoutSession> GetAllFromDatabase()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<WorkoutSession> GetAllForkWorkout(int workoutId)
        {
            return DatabaseContext.SetOwnable<WorkoutSession>().Where(w => w.Workout.Id == workoutId);
        }


        #endregion
    }
}