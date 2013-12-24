using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;
using Model.Definitions;

namespace Services.Base
{
    public abstract class BaseService
    {
        public BaseService(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory, ICurrentUser user)
        {
            Repository = repositoryFactory;
            Mapper = mapperFactory;
            Repository.SetUser(user);
        }

        protected IRepositoryFactory Repository { get; private set; }
        protected IMapperFactory Mapper { get; private set; }


    }
}