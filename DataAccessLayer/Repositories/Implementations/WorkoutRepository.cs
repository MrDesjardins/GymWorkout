using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Base;
using DataAccessLayer.Repositories.Definitions;
using Model;
using Shared;

namespace DataAccessLayer.Repositories.Implementations
{
    public class WorkoutRepository : BaseRepository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        #region Implementation of IRepository<Workout>

        public override IQueryable<Workout> GetAll()
        {
            return DatabaseContext.SetOwnable<Workout>().Include(x => x.Sessions);
        }

        public override Workout Get(int id)
        {
            try
            {
                var workout = DatabaseContext.SetOwnable<Workout>()
                    .Include(x => x.Sessions)
                    .Include(d=>d.Sessions.Select(dd=>dd.WorkoutSessionExercises))
                    .Include(d=>d.Sessions.Select(dd=>dd.WorkoutSessionExercises.Select(ddd=>ddd.Exercise)))
                    .AsNoTracking()
                    .Single(c => c.Id == id);

                return workout;
            }
            catch (InvalidOperationException ioe)
            {
                throw new DataNotFoundException(ioe);
            }
        }

        public override int Insert(Workout entity)
        {
            int count = -1;
            using (base.DatabaseContext.RemoveValidation())
            {
               
                RemoveWorkoutSessionExercisesNotUsedAnymore(entity);
                SetupWorkoutForRepository(entity);
                DatabaseContext.InsertOwnable(entity);
                DatabaseContext.Entry(entity).CurrentValues.SetValues(entity);
                count = DatabaseContext.SaveChanges();
               
            }
            return count;
        }



        public override int Update(Workout entity)
        {
            int count = -1;
            using (base.DatabaseContext.RemoveValidation())
            {
              
                RemoveWorkoutSessionExercisesNotUsedAnymore(entity);
                SetupWorkoutForRepository(entity);
                DatabaseContext.UpdateOwnable(entity);
                var ent = DatabaseContext.SetOwnable<Workout>().Local.SingleOrDefault(d => d.Id == entity.Id);
                DatabaseContext.Entry(ent).CurrentValues.SetValues(entity);
                count = DatabaseContext.SaveChanges();
                 
            }
            return count;
         
        }

        private void RemoveWorkoutSessionExercisesNotUsedAnymore(Workout entity)
        {
            var listWorkoutSessionExerciseThatStillRemain = entity.Sessions.SelectMany(d=>d.WorkoutSessionExercises).Select(d=>d.Id).ToArray();
            var workoutSessionExerciseFromDatabase = DatabaseContext.SetOwnable<WorkoutSessionExercise>()
                .Where(d=>!listWorkoutSessionExerciseThatStillRemain.Contains(d.Id) &&  d.WorkoutSession.Workout.Id == entity.Id)
                ;
            foreach (var sessionExercise in workoutSessionExerciseFromDatabase)
            {
              DatabaseContext.SetOwnable<WorkoutSessionExercise>().Remove(sessionExercise);
            }
        }

        private void SetupWorkoutForRepository(Workout entity)
        {
            if (entity.Sessions != null)
            {
                foreach (var workoutSession in entity.Sessions)
                {
                    if (workoutSession.WorkoutSessionExercises != null)
                    {
                        foreach (var workoutSessionExercise in workoutSession.WorkoutSessionExercises)
                        {
                            if (workoutSessionExercise.Exercise != null)
                            {
                                if (workoutSessionExercise.Exercise.Id == BaseModel.NOT_INITIALIZED)
                                {
                                    workoutSessionExercise.Exercise = null; //Should never go there
                                }
                                else
                                {
                                    if (DatabaseContext.Set<Exercise>().Local.All(e => e.Id != workoutSessionExercise.Exercise.Id))
                                    {
                                        workoutSessionExercise.Exercise = DatabaseContext.Set<Exercise>().Attach(workoutSessionExercise.Exercise);
                                    }
                                    else
                                    {
                                        workoutSessionExercise.Exercise = DatabaseContext.Set<Exercise>().Local.Single(e => e.Id == workoutSessionExercise.Exercise.Id);    
                                    }
                                }
                                    
                            }

                            if (workoutSessionExercise.Id == BaseModel.NOT_INITIALIZED)
                            {
                                //New workout session exercise
                                DatabaseContext.SetOwnable<WorkoutSessionExercise>().Add(workoutSessionExercise);
                            }
                            else
                            {
                                if (DatabaseContext.Set<WorkoutSessionExercise>().Local.All(e => e.Id != workoutSessionExercise.Id))
                                {
                                    DatabaseContext.SetOwnable<WorkoutSessionExercise>().Attach(workoutSessionExercise);
                                }
                                DatabaseContext.Entry(workoutSessionExercise).Property(d => d.Order).IsModified = true;
                            }
                        }
                        
                        if (workoutSession.Id == BaseModel.NOT_INITIALIZED)
                        {
                            DatabaseContext.SetOwnable<WorkoutSession>().Add(workoutSession);
                        }
                        else
                        {
                            var dbEntry = DatabaseContext.SetOwnable<WorkoutSession>().Local.SingleOrDefault(e => e.Id == workoutSession.Id);
                            if (dbEntry==null)
                            {
                                dbEntry = DatabaseContext.SetOwnable<WorkoutSession>().Attach(workoutSession);
                            }
                            
                        }

                        foreach (var workoutSessionExercise in workoutSession.WorkoutSessionExercises)
                        {
                            workoutSessionExercise.WorkoutSession = DatabaseContext.SetOwnable<WorkoutSession>().Local.SingleOrDefault(d=>d.Id == workoutSession.Id);
                        }

                    }
                }
            }
        }



        public override int Delete(Workout entity)
        {
            DatabaseContext.Delete(entity);
            return DatabaseContext.SaveChanges();
        }

        public override IQueryable<Workout> GetAllFromDatabase()
        {
            throw new NotImplementedException();
        }

        public int GetAmountWorkoutForCurrentMonth()
        {
            return DatabaseContext.SetOwnable<Workout>().Count(w => w.StartTime.Month == DateTime.Now.Month && w.StartTime.Year == DateTime.Now.Year);
        }

        public int UpdateSessionOrderOnly(Workout workout)
        {

            Workout workoutFromDatabase;
            try
            {
                workoutFromDatabase = DatabaseContext.SetOwnable<Workout>().Include(d => d.Sessions).Single(d => d.Id == workout.Id); //Required because we want to be sure we do have a workout for the logged user
            }
            catch (InvalidOperationException ioe)
            {
                throw new DataNotFoundException(ioe);
            }
            foreach (var workoutSessionExerciseFromDatabase in workoutFromDatabase.Sessions)
            {
                var workoutSessionFromForm = workout.Sessions.SingleOrDefault(d => d.Id == workoutSessionExerciseFromDatabase.Id);
                if (workoutSessionFromForm!= null)
                {
                    workoutSessionExerciseFromDatabase.Order = workoutSessionFromForm.Order;
                    DatabaseContext.Entry(workoutSessionExerciseFromDatabase).Property(d => d.Order).IsModified = true;
                }
            }
            return DatabaseContext.SaveChangesWithoutValidation();            

        }

        #endregion
    }
}