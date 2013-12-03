using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace WorkoutPlannerApi.IocConfiguration
{
    internal class ScopeContainer : IDependencyScope
    {
        protected readonly IUnityContainer _container;

        public ScopeContainer(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this._container = container;
        }

        public object GetService(Type serviceType)
        {

            if (!_container.IsRegistered(serviceType))
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return null;
                }
            }
            return _container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.IsRegistered(serviceType) ? _container.ResolveAll(serviceType) : new List<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}