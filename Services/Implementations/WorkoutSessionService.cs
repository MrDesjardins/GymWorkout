using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;
using Model;
using Model.Definitions;
using Services.Base;
using Services.Definitions;
using ViewModels;

namespace Services.Implementations
{
    public class WorkoutSessionService : BaseService, IWorkoutSessionService
    {
        public WorkoutSessionService(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory, ICurrentUser user)
            : base(repositoryFactory, mapperFactory, user)
        {
        }

        #region Implementation of IService<Workout>

        public IEnumerable<WorkoutSession> GetAll()
        {
            List<WorkoutSession> listModel = Repository.WorkoutSession.GetAll().ToList();
            return listModel;
        }

        public WorkoutSession New()
        {
            return new WorkoutSession();
        }

        public WorkoutSession Get(WorkoutSession model)
        {
            WorkoutSession modelToBound = Repository.WorkoutSession.Get(model.Id);
            return modelToBound;
        }

        public WorkoutSession Create(WorkoutSession model)
        {
            Repository.WorkoutSession.Insert(model);
            return model;
        }

        public int Update(WorkoutSession model, params Expression<Func<WorkoutSession,object>>[] properties)
        {
            return Repository.WorkoutSession.Update(model, properties);
        }

        public int Update(WorkoutSession model)
        {
            return Repository.WorkoutSession.Update(model);
        }

        public int Delete(WorkoutSession model)
        {
            return Repository.WorkoutSession.Delete(model);
        }

        public IEnumerable<WorkoutSessionViewModel> GetAllForWorkout(int workoutId)
        {
            List<WorkoutSession> list = Repository.WorkoutSession.GetAllForkWorkout(workoutId).ToList();
            return Mapper.Map<List<WorkoutSession>, List<WorkoutSessionViewModel>>(list);
        }

        public WorkoutViewModel GetWorkoutWithWorkoutSession(int workoutId)
        {
            Workout workout = Repository.Workout.Get(workoutId);
            return Mapper.Map<Workout, WorkoutViewModel>(workout);
        }



        #endregion
    }
}