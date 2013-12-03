using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessLogic.Validations;
using DataAccessLayer.Repositories.Base;
using Mappers.Factory;
using Model;
using Services.Base;
using Services.Definitions;
using WorkoutPlanner.Validations;

namespace Services.Implementations
{
    public class WorkoutService : BaseService, IWorkoutService
    {
        public WorkoutService(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory) : base(repositoryFactory, mapperFactory)
        {
        }

        #region Implementation of IService<Workout>

        public IEnumerable<Workout> GetAll()
        {
            var listModel = Repository.Workout.GetAll().ToList();
            return listModel;
        }

        public Workout New()
        {
            return new Workout();
        }

        public Workout Get(Workout model)
        {
            var modelToBound = Repository.Workout.Get(model.Id);
            return modelToBound;
        }

        public Workout Create(Workout model)
        {
            int amountWorkout = Repository.Workout.GetAmountWorkoutForCurrentMonth();
            if (amountWorkout>3)//More than 3 workouts done without premium account
            {
                throw new ValidationErrors(new GeneralError("You have reach the limit of 3 workouts per month, you need premium or wait the next month"));
            }
            SetWorkoutSessionExerciseOrder(model);
            Repository.Workout.Insert(model);
            return model;
        }

        private static void SetWorkoutSessionExerciseOrder(Workout model)
        {
            foreach (var workoutSession in model.Sessions)
            {
                int index = 1;
                foreach (var workoutSessionExercise in workoutSession.WorkoutSessionExercises)
                {
                    workoutSessionExercise.Order = index++;
                }
            }
        }

        public int Update(Workout model)
        {
            SetWorkoutSessionExerciseOrder(model);
            return Repository.Workout.Update(model);
        }

        public int Update(Workout model, params Expression<Func<Workout, object>>[] properties)
        {
            return Repository.Workout.Update(model, properties);
        }

        public int Delete(Workout model)
        {
            return Repository.Workout.Delete(model);
        }

        public int UpdateSessionOrderOnly(Workout workout)
        {
            return Repository.Workout.UpdateSessionOrderOnly(workout);
        }

        #endregion
    }
}