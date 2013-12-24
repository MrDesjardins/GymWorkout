using System;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Sessions;
using DataAccessLayer;
using Mappers;
using Mappers.Factory;
using Services.Base;
using WorkoutPlanner.Controllers.Base;

namespace WorkoutPlanner.Controllers
{
    public class HomeController : BaseController<NullReferenceException,NullReferenceException>
    {
        public HomeController(IServiceFactory serviceFactory
                            , IMapperFactory mapperFactory
                            , IUserProvider userProvider
                            , ISessionHandler sessionHandler) : base(serviceFactory, mapperFactory, userProvider, sessionHandler)
        {
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
