using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Mappers;
using Mappers.Factory;
using Services.Base;

namespace WorkoutPlannerApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private readonly IMapperFactory _mapperFactory;
        private readonly IServiceFactory _serviceFactory;

        protected BaseApiController(IServiceFactory serviceFactory, IMapperFactory mapperFactory)
        {
            _mapperFactory = mapperFactory;
            _serviceFactory = serviceFactory;
        }

        protected IServiceFactory ServiceFactory
        {
            get { return _serviceFactory; }
        }

        protected IMapperFactory MapperFactory
        {
            get { return _mapperFactory; }
        }
        
    }
}