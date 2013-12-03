using System;
using System.Net;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Routing;

namespace WorkoutPlanner.Views
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute, IFilter
    {
        public AuthorizeAttribute()
        {
            ErrorArea = string.Empty;
            ErrorController = "Error";
            ErrorAction = "Index";
        }

        public string ErrorArea { get; set; }
        public string ErrorController { get; set; }
        public string ErrorAction { get; set; }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (AuthorizeCore(filterContext.HttpContext))
                return;
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (ErrorController != null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = ErrorAction,
                        controller = ErrorController,
                        area = ErrorArea
                    }));
                }
                else
                {
                    filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
                }
            }

            //base.OnAuthorization(filterContext);

            
            //if (filterContext.HttpContext.Request.IsAuthenticated)
            //{
            //    if (!string.IsNullOrEmpty(this.Roles) && !filterContext.HttpContext.User.IsInRole(this.Roles))
            //    {
            //        filterContext.Result = new HttpStatusCodeResult((int) HttpStatusCode.Forbidden);
            //    }


            //}

            //if (AuthorizeCore(filterContext.HttpContext))
            //    return;

            //if (ErrorController != null)
            //{
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            //                                                                                    {
            //                                                                                        action =ErrorAction,
            //                                                                                        controller =ErrorController,
            //                                                                                        area = ErrorArea
            //                                                                                    }));
            //}
            //else
            //{
            //    filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            //}
            


        }

    }
}