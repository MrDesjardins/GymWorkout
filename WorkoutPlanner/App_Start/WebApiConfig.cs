using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WorkoutPlanner
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            FixCircularReference(config);
            config.Filters.Add(new AuthorizeAttribute());
            //config.Filters.Add(new Views.AuthorizeAttribute());
        }
            
        private static void FixCircularReference(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
