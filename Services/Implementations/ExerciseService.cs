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
    public class ExerciseService : BaseService, IExerciseService
    {
        private readonly IMuscleService _muscleService;

        public ExerciseService(IRepositoryFactory repositoryFactory
                             , IMapperFactory mapperFactory
                             , IMuscleService muscleService)
            : base(repositoryFactory, mapperFactory)
        {
            _muscleService = muscleService;
        }

    

        #region Implementation of IService<Workout>


        public Exercise New()
        {
            var vm = new Exercise();
            //ListMuscles = _muscleService.GetAllSelector()
            return vm;
        }

        public Exercise Get(Exercise model)
        {
            var modelFromDatabase = Repository.Exercise.Get(model.Id);
            return modelFromDatabase;
        }

        public Exercise Create(Exercise model)
        {
            Repository.Exercise.Insert(model);
            return model;
        }

        public int Update(Exercise model)
        {
            return Repository.Exercise.Update(model);
        }

        public int Update(Exercise model, params Expression<Func<Exercise, object>>[] properties)
        {
            return Repository.Exercise.Update(model, properties);
        }

        public int Delete(Exercise model)
        {
            return Repository.Exercise.Delete(model);
        }

        public IEnumerable<Exercise> GetAll()
        {
            var fullyLoadedFromDatabase = Repository.Exercise.GetAllFromDatabase();
            return fullyLoadedFromDatabase;
        }

        #endregion
    }
}