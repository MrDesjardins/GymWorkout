using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace Setup.Ioc.IocConfiguration
{
    public class IoCContainer : ScopeContainer, IDependencyResolver
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