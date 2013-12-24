using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;
using Model;
using Model.Definitions;
using Services.Base;
using Services.Definitions;
using ViewModels.Selectors.Implementations;
using WorkoutPlanner.ViewModels;

namespace Services.Implementations
{
    public class MuscleService : BaseService, IMuscleService
    {
        public MuscleService(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory, ICurrentUser user)
            : base(repositoryFactory, mapperFactory, user)
        {
        }

        public IEnumerable<Muscle> GetAll()
        {
            var listModel = Repository.Muscle.GetAll().ToList();
            return listModel;
        }

        public Muscle New()
        {
            var vm = new Muscle();
            return vm;
        }

        public Muscle Get(Muscle model)
        {
            var modelFromDatabase = Repository.Muscle.Get(model.Id);
            return modelFromDatabase;
        }

        public Muscle Create(Muscle model)
        {
            Repository.Muscle.Insert(model);
            return model;
        }

        public int Update(Muscle model)
        {
            try
            {
                return Repository.Muscle.Update(model);
            }
            catch (DatabaseConcurrencyException d)
            {
                throw new ConcurrencyException(d);
            }
        }

        public int Update(Muscle model, params Expression<Func<Muscle, object>>[] properties)
        {
            return Repository.Muscle.Update(model, properties);
        }

        public int Delete(Muscle model)
        {
            return Repository.Muscle.Delete(model);
        }

        public IEnumerable<MuscleSelector> GetAllSelector()
        {
            var allMuscles = Repository.Muscle.GetAll().ToList();
            return Mapper.Map<List<Muscle>, List<MuscleSelector>>(allMuscles);
        }
    }
}