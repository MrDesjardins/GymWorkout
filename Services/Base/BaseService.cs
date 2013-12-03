using DataAccessLayer.Repositories.Base;
using Mappers;
using Mappers.Factory;

namespace Services.Base
{
    public abstract class BaseService
    {
        public BaseService(IRepositoryFactory repositoryFactory, IMapperFactory mapperFactory)
        {
            Repository = repositoryFactory;
            Mapper = mapperFactory;
        }

        protected IRepositoryFactory Repository { get; private set; }
        protected IMapperFactory Mapper { get; private set; }
    }
}