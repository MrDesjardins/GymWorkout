using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DataAccessLayer.Database;
using Mappers;
using Mappers.Base;
using Mappers.Factory;
using Microsoft.Practices.Unity;
using Setup.Ioc;
using WorkoutPlanner.Ioc;
using WorkoutPlannerApi.IocConfiguration;

namespace WorkoutPlannerApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityConfiguration.Initialize();
            MapperConfiguration.Initialize(UnityConfiguration.Container.Resolve<IMapperFactory>());
            UnityConfiguration.Container.Resolve<IDatabaseContext>().InitializeDatabase();
            GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(UnityConfiguration.Container);
        }
    }
}