using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WorkoutPlanner.Controllers
{
    public class ErrorController : Controller
    {
        public const string ERROR_VIEW = "Error404";
        public const string NOACCESS_VIEW = "Error403";
        public const string UNKNOWN_VIEW = "Error401";

        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return GetActionResult(ERROR_VIEW);
        }
        public ActionResult NoAccess()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return GetActionResult(NOACCESS_VIEW);
        }
        public ActionResult Unknown()
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return GetActionResult(UNKNOWN_VIEW);
        }
        private ActionResult GetActionResult(string errorViewName)
        {
            ActionResult result;
            object model = Request.Url.PathAndQuery;

            if (Request.IsAjaxRequest())
            {
                if (Request.ContentType == "application/json")
                {
                    result = Json(new {Error = "Server cannot threat your request."});
                }
                else
                {
                    result = PartialView(errorViewName, model);
                }
            }
            else
            {
                result = View(errorViewName, model);
            }
            return result;
        }


    }
}
