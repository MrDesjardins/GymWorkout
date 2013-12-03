using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Base;
using DataAccessLayer.Repositories.Definitions;
using Model;
using Shared;

namespace DataAccessLayer.Repositories.Implementations
{
    public class WorkoutSessionExerciseRepository : BaseRepository<WorkoutSessionExercise>, IWorkoutSessionExerciseRepository
    {
        public WorkoutSessionExerciseRepository(IDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        #region Implementation of IRepository

        public override IQueryable<WorkoutSessionExercise> GetAll()
        {
            return DatabaseContext.SetOwnable<WorkoutSessionExercise>();
        }

        public override WorkoutSessionExercise Get(int id)
        {
            return DatabaseContext.SetOwnable<WorkoutSessionExercise>()
                .Include(d=>d.Exercise)
                .Include(d=>d.WorkoutSession)
                .Single(c => c.Id == id);
        }

        public override int Insert(WorkoutSessionExercise entity)
        {
            DatabaseContext.Attach(entity.Exercise);
            DatabaseContext.AttachOwnable(entity.WorkoutSession);
            DatabaseContext.InsertOwnable(entity);
            return DatabaseContext.SaveChanges();
        }

        public override int Update(WorkoutSessionExercise entity)
        {
            WorkoutSessionExercise fromDatabase = Get(entity.Id);
            DatabaseContext.Entry(fromDatabase).CurrentValues.SetValues(entity);
            DatabaseContext.Entry(fromDatabase).State = EntityState.Modified;

            if (entity.Exercise != null)
            {
                var localExercise = DatabaseContext.Set<Exercise>().Local.SingleOrDefault(e => e.Id == entity.Exercise.Id);
                if (localExercise==null)
                {
                    DatabaseContext.Set<Exercise>().Attach(entity.Exercise);
                }
                fromDatabase.Exercise = localExercise;
            }
            else
            {
                fromDatabase.Exercise = null;    
            }
            

            return DatabaseContext.SaveChanges();
        }

        public override int Delete(WorkoutSessionExercise entity)
        {
            DatabaseContext.SetOwnable<WorkoutSessionExercise>().Remove(entity);
            return DatabaseContext.SaveChanges();
        }

        public override IQueryable<WorkoutSessionExercise> GetAllFromDatabase()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<WorkoutSessionExercise> GetForWorkoutSession(int workoutSessionId)
        {
            return DatabaseContext.SetOwnable<WorkoutSessionExercise>()
                    .Where(w => w.WorkoutSession.Id  == workoutSessionId);
        }

        public int UpdatePartial(WorkoutSessionExercise entity)
        {
            //DatabaseContext.AttachOwnable(entity);
            //DatabaseContext.Entry(entity).Property(d => d.Repetitions).IsModified = true;
            //DatabaseContext.Entry(entity).Property(d => d.Weights).IsModified = true;
            //DatabaseContext.Entry(entity).Property(d => d.RestBetweenSetTicks).IsModified = true;
            //DatabaseContext.Entry(entity).Property(d => d.Tempo).IsModified = true;
            //Get the entity from the database instead of the code above for security purpose. By getting
            //the entity, we are the exercise belong to the user who request the update
            WorkoutSessionExercise workoutExerciseFromDatabase;
            try{
                workoutExerciseFromDatabase = DatabaseContext.SetOwnable<WorkoutSessionExercise>().Single(d => d.Id == entity.Id);
            }
            catch (InvalidOperationException ioe)
            {
                throw new DataNotFoundException(ioe);
            }
            workoutExerciseFromDatabase.Repetitions = entity.Repetitions;
            workoutExerciseFromDatabase.Weights = entity.Weights;
            workoutExerciseFromDatabase.RestBetweenSetTicks = entity.RestBetweenSetTicks;
            workoutExerciseFromDatabase.Tempo = entity.Tempo;
            DatabaseContext.UpdateOwnable(workoutExerciseFromDatabase);
            return DatabaseContext.SaveChanges();
        }


        #endregion
    }
}