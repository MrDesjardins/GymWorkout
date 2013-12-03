using System.Web;
using System.Web.Mvc;

namespace WorkoutPlanner
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Views.AuthorizeAttribute());
            //filters.Add(new AuthorizeAttribute());
        }
    }
}