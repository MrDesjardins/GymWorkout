using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace WorkoutPlannerApi.IocConfiguration
{
    internal class IoCContainer : ScopeContainer, IDependencyResolver
    {
        public IoCContainer(IUnityContainer container):base(container)
        {
        }


        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new ScopeContainer(child);
        }
    }
}