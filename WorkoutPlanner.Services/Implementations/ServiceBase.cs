using BusinessLogic;
using Mappers.Factory;
using Model;
using Model.Definitions;
using Services.Base;
using Shared.Log;

namespace WorkoutPlanner.Services.Implementations
{
    public class ServiceBase    
    {
        private readonly IMapperFactory _mapperFactory;
        private readonly MapperService.Factory.IMapperFactory _mapperServiceFactory;
        private readonly IServiceFactory _serviceFactory;
        private readonly IUserProvider _userProvider;
        private readonly ILog _log;

        protected ServiceBase(IServiceFactory serviceFactory, IMapperFactory mapperFactory, MapperService.Factory.IMapperFactory mapperServiceFactory, IUserProvider userProvider, ILog log)
        {
            _serviceFactory = serviceFactory;
            _mapperServiceFactory = mapperServiceFactory;
            _mapperFactory = mapperFactory;
            _userProvider = userProvider;
            _log = log;
        }
        protected IServiceFactory ServiceFactory
        {
            get { return _serviceFactory; }
        }

        protected IMapperFactory MapperFactory
        {
            get { return _mapperFactory; }
        }

        protected MapperService.Factory.IMapperFactory MapperServiceFactory {
            get { return _mapperServiceFactory; }
        }

        protected ICurrentUser CurrentUser
        {
            get
            {
                //[P] : This won't be like that for WCF Service

                ICurrentUser currentUserFromProvider = _userProvider.Account;
                UserProfile fullUserProfile = _serviceFactory.Account.GetByUserName(currentUserFromProvider.UserName);
                if (fullUserProfile == null)//Case of a non identified user
                    fullUserProfile = new UserProfile();
                return fullUserProfile;
                
            }
        }
    }
}