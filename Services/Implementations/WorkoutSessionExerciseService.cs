using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;
using Model;
using Services.Base;
using Services.Definitions;
using ViewModels;
using ViewModels.Selectors.Implementations;

namespace Services.Implementations
{
    public class WorkoutSessionExerciseService : BaseService, IWorkoutSessionExerciseService
    {
        public WorkoutSessionExerciseService(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory)
            : base(repositoryFactory, mapperFactory)
        {
        }

        #region Implementation of IService<Workout>

        public IEnumerable<WorkoutSessionExercise> GetAll()
        {
            var listModel = Repository.WorkoutSessionExercise.GetAll().ToList();
            return listModel;
        }

        public WorkoutSessionExercise New()
        {
            
            return new WorkoutSessionExercise();
        }

        public WorkoutSessionExercise Get(WorkoutSessionExercise model)
        {
            var modelToBound = Repository.WorkoutSessionExercise.Get(model.Id);
            return modelToBound;
        }

        public WorkoutSessionExercise Create(WorkoutSessionExercise model)
        {
            Repository.WorkoutSessionExercise.Insert(model);
            return model;
        }

        public int Update(WorkoutSessionExercise model)
        {
            return Repository.WorkoutSessionExercise.Update(model);
        }

        public int Update(WorkoutSessionExercise model, params Expression<Func<WorkoutSessionExercise, object>>[] properties)
        {
            return Repository.WorkoutSessionExercise.Update(model, properties);
        }

        public int Delete(WorkoutSessionExercise model)
        {
            return Repository.WorkoutSessionExercise.Delete(model);
        }

        public WorkoutSessionViewModel GetWorkoutSessionWithWorkoutSessionExercise(int workoutSessionId)
        {
            WorkoutSession workoutSession = Repository.WorkoutSession.Get(workoutSessionId);
            return Mapper.Map<WorkoutSession, WorkoutSessionViewModel>(workoutSession);
        }

        public int UpdatePartial(WorkoutSessionExercise model)
        {
            return Repository.WorkoutSessionExercise.UpdatePartial(model);
        }

        #endregion
    }
}