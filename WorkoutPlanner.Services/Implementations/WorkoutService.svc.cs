using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessLogic;
using Dto;
using Mappers.Factory;
using Model;
using Services.Base;
using Shared.Log;
using WorkoutPlanner.Services.Contracts.Services;

namespace WorkoutPlanner.Services.Implementations
{

    public class WorkoutService : ServiceBase, IWorkoutService
    {


        public WorkoutService(IServiceFactory serviceFactory
                                  , IMapperFactory mapperFactory
                                  , MapperService.Factory.IMapperFactory mapperServiceFactory
                                  , IUserProvider userProvider
                                  , ILog log)
            : base(serviceFactory, mapperFactory, mapperServiceFactory, userProvider, log)
        {
            
        }

        public IEnumerable<FlattenWorkout> GetAllWorkout()
        {
            var x = base.CurrentUser.UserId;
            var exercises = ServiceFactory.Workout.GetAll();
            var exercisesMapped = MapperFactory.Map<IEnumerable<Workout>, IEnumerable<FlattenWorkout>>(exercises);
            return exercisesMapped;
        }

        public FlattenWorkout GetWorkout(int uniqueIdentifier)
        {
            var exercises = ServiceFactory.Workout.Get(new Workout { Id = uniqueIdentifier });
            var exercisesMapped = MapperFactory.Map<Workout, FlattenWorkout>(exercises);
            return exercisesMapped;
        }
    }
}
