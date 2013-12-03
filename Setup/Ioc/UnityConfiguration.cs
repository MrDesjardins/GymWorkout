using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Sessions;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Base;
using DataAccessLayer.Repositories.Definitions;
using DataAccessLayer.Repositories.Implementations;
using Mappers.Factory;
using Microsoft.Practices.Unity;
using Model;
using Model.Definitions;
using Services;
using Services.Base;
using Services.Definitions;
using Services.Implementations;
using Setup.Sessions;
using Shared.Log;
using Unity.Mvc3;

namespace Setup.Ioc
{
    public static class UnityConfiguration
    {
        public static IUnityContainer Container;
        public static void Initialize()
        {
            Container = new UnityContainer()
                .RegisterType<ICurrentUser, UserProfile>()
                .RegisterType<IUserProvider, WebUserProvider>()
                .RegisterType<ISessionHandler, HttpSessionHandler>()
                .RegisterType<IDatabaseContext, DatabaseContext>()
                .RegisterType<IRepositoryFactory, RepositoryFactory>()
                .RegisterType<IServiceFactory, ServiceFactory>()
                .RegisterType<IMapperFactory, MapperFactory>()
                .RegisterType<ILog, LogFile>()
                ;
            InitializeRepositories();
            InitializeServices();
            InitializeWebServices();
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }

        private static void InitializeWebServices()
        {
            Container.RegisterType<MapperService.Factory.IMapperFactory, MapperService.Factory.MapperFactory>();
        }

        private static void InitializeServices()
        {
              Container
                 .RegisterType<IAccountService, AccountService>()
                 .RegisterType<IWorkoutService, WorkoutService>()
                 .RegisterType<IWorkoutSessionService, WorkoutSessionService>()
                 .RegisterType<IWorkoutSessionExerciseService, WorkoutSessionExerciseService>()
                 .RegisterType<IExerciseService, ExerciseService>()
                 .RegisterType<IMuscleService, MuscleService>()
                 ;
        }

        private static void InitializeRepositories()
        {
            Container
                .RegisterType<IUserProfileRepository, UserProfileRepository>()
                .RegisterType<IWorkoutRepository, WorkoutRepository>()
                .RegisterType<IWorkoutSessionRepository, WorkoutSessionRepository>()
                .RegisterType<IWorkoutSessionExerciseRepository, WorkoutSessionExerciseRepository>()
                .RegisterType<IExerciseRepository, ExerciseRepository>()
                .RegisterType<IMuscleRepository, MuscleRepository>()
                ;
        }
    }
}