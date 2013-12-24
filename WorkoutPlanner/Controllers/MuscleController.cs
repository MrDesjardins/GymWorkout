using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Sessions;
using BusinessLogic.Validations;
using DataAccessLayer;
using Mappers;
using Mappers.Factory;
using Model;
using Services.Base;
using Services.Implementations;
using WorkoutPlanner.Controllers.Base;
using WorkoutPlanner.ViewModels;

namespace WorkoutPlanner.Controllers
{
    public class MuscleController : BaseController<Muscle, MuscleViewModel>
    {
        public MuscleController(IServiceFactory serviceFactory, IMapperFactory mapperFactory, IUserProvider userProvider, ISessionHandler sessionHandler) : base(serviceFactory, mapperFactory, userProvider, sessionHandler)
        {
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var x = ServiceFactory.Muscle.GetAll();
            var viewModel = MapperFactory.Muscle.GetViewModelList(x);
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var x = ServiceFactory.Muscle.Get(new Muscle{Id=id});
            var viewModel = MapperFactory.Muscle.GetViewModel(x);
            return View("Details", viewModel);
        }

        [HttpGet]
        [Views.Authorize(Roles = Roles.ADMINISTRATOR)]
        public ActionResult Create()
        {
            var x = ServiceFactory.Muscle.New();
            var viewModel = MapperFactory.Muscle.GetViewModel(x);
            return View("Create", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMINISTRATOR)]
        public ActionResult Create(MuscleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fromDatabase = ServiceFactory.Muscle.Create(Model);
                    var viewModelFromDatabase = MapperFactory.Muscle.GetViewModel(fromDatabase);
                    return View("Details", viewModelFromDatabase);
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }
            return View(viewModel);
        }
        
        [HttpGet]
        [Authorize(Roles = Roles.ADMINISTRATOR)]
        public ActionResult Edit(int id)
        {
            var x = ServiceFactory.Muscle.Get(new Muscle{Id=id});
            var viewModel = MapperFactory.Muscle.GetViewModel(x);
            return View("Edit", viewModel);
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.ADMINISTRATOR)]
        public ActionResult Edit(MuscleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ServiceFactory.Muscle.Update(Model);
                }
                catch (ConcurrencyException e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View("Edit", viewModel);
                }
                
            }
            var x = ServiceFactory.Muscle.Get(Model);
            var vm = MapperFactory.Muscle.GetViewModel(x);
            return View("Edit", vm);
        }

        [HttpGet]
        [Authorize(Roles = Roles.ADMINISTRATOR)]
        public ActionResult Delete(int id)
        {
            ServiceFactory.Muscle.Delete(new Muscle{Id=id});
            return RedirectToAction("Index");
        }
    }
}
