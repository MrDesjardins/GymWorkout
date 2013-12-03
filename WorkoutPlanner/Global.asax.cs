using System;
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
using Setup.Ioc.IocConfiguration;
using WorkoutPlanner.Controllers;
using WorkoutPlanner.Ioc;


namespace WorkoutPlanner
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            
            UnityConfiguration.Initialize();
            MapperConfiguration.Initialize(UnityConfiguration.Container.Resolve<IMapperFactory>());

            UnityConfiguration.Container.Resolve<IDatabaseContext>().InitializeDatabase();
            GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(UnityConfiguration.Container);
   }

        protected void Session_Start(Object sender, EventArgs e)
        {
           
        }
        protected void Application_Error(object sender, EventArgs e)
        {
    
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            //Ajax redirection fix
            var context = (HttpApplication)sender;
            var contextWrapper = new HttpContextWrapper(this.Context);
            context.Response.SuppressFormsAuthenticationRedirect = true;


            var statusCode = Context.Response.StatusCode;
            if (statusCode == 404 || statusCode == 500)
            {
                Response.Clear();

                var routingData = new RouteData();
                //rd.DataTokens["area"] = "AreaName"; // In case controller is in another area
                routingData.Values["controller"] = "Error";
                routingData.Values["action"] = "NotFound";
                IController c = new ErrorController();
              
                c.Execute(new RequestContext(new HttpContextWrapper(Context), routingData));
            }
            
        }

    }


   


}