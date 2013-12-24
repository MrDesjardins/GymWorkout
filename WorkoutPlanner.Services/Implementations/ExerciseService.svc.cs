using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Sessions;
using DataAccessLayer;
using Dto;
using MapperService.Factory;
using Mappers.Factory;
using Model;
using Services.Base;
using Shared.Log;
using WorkoutPlanner.Services.Contracts.Services;
using IMapperFactory = Mappers.Factory.IMapperFactory;

namespace WorkoutPlanner.Services.Implementations
{
    public class ExerciseService : ServiceBase, IExerciseService
    {
  

        public ExerciseService(IServiceFactory serviceFactory
                                  , IMapperFactory mapperFactory
                                  , MapperService.Factory.IMapperFactory mapperServiceFactory
                                  , IUserProvider userProvider
                                  , ILog log)
            : base(serviceFactory, mapperFactory, mapperServiceFactory, userProvider, log)
        {
            
        }

        public IEnumerable<FlattenExercise> GetAllExercise()
        {
            var exercises = ServiceFactory.Exercise.GetAll();
            var exercisesMapped = MapperFactory.Map<IEnumerable<Exercise>, IEnumerable<FlattenExercise>>(exercises);
            return exercisesMapped;
        }

        public FlattenExercise GetExercise(int uniqueIdentifier)
        {
            var exercises = ServiceFactory.Exercise.Get(new Exercise { Id=uniqueIdentifier});
            var exercisesMapped = MapperFactory.Map<Exercise, FlattenExercise>(exercises);
            return exercisesMapped;
        }
    }

    
}
