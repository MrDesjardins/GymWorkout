using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Sessions;
using BusinessLogic.Validations;
using DataAccessLayer;
using Mappers;
using Mappers.Factory;
using Model;
using Newtonsoft.Json;
using Services.Base;
using Shared;
using ViewModels;
using WorkoutPlanner.Controllers.Base;
using Dto;

namespace WorkoutPlanner.Controllers
{
    public class WorkoutSessionController : BaseController<WorkoutSession, WorkoutSessionViewModel>
    {
        public WorkoutSessionController(IServiceFactory serviceFactory
                                 , IMapperFactory mapperFactory
                                 , IUserProvider userProvider
                                 , ISessionHandler sessionHandler) : base(serviceFactory, mapperFactory, userProvider, sessionHandler)

        {

        }

        public ActionResult IndexForWorkout(int workoutId)
        {
            var x = ServiceFactory.WorkoutSession.GetWorkoutWithWorkoutSession(workoutId);
            return View("Index",x);
        }
 
        [HttpGet]
        public ActionResult Details(int id)
        {
            var x = ServiceFactory.WorkoutSession.Get(new WorkoutSession { Id = id });
            var viewModel = MapperFactory.WorkoutSession.GetViewModel(x);
            return View("Details",viewModel);
        }

        [HttpGet]
        public ActionResult CreateForWorkout(int idWorkout)
        {
            var newModel = ServiceFactory.Workout.New();
            var x = new WorkoutSessionViewModel { WorkoutId = idWorkout };
            return View("CreateForWorkout",x);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(WorkoutSessionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ServiceFactory.WorkoutSession.Create(Model);
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }
            return View("CreateForWorkout",viewModel);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var x = ServiceFactory.WorkoutSession.Get(new WorkoutSession { Id = id });
            var viewModel = MapperFactory.WorkoutSession.GetViewModel(x);
            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(WorkoutSessionViewModel viewModel)
        {
            var fromDatabase = ServiceFactory.WorkoutSession.Get(Model);
            Model.Workout.Id = fromDatabase.Workout.Id; //We assure that we do not modify the workout id
            if (ModelState.IsValid)
            {
                ServiceFactory.WorkoutSession.Update(Model);
            }
            viewModel = MapperFactory.WorkoutSession.GetViewModel(Model);
            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult EditName(WorkoutSessionViewModel viewModel)
        {
            ServiceFactory.WorkoutSession.Update(Model, d=>d.Name);
            return RedirectToAction("Details", new { id = Model.Id });
        }

        [HttpGet]
        public ActionResult Delete(int id, int workoutId)
        {
            ServiceFactory.WorkoutSession.Delete(new WorkoutSession { Id = id });
            return RedirectToAction("IndexForWorkout", new { workoutId = workoutId });
        }

        [HttpPost]
        public JsonResult SaveWorkoutSessionOrder(WorkoutSessionOrder workoutSessionOrder)
        {

            int order = 1;
            var workout = new Workout { Id = workoutSessionOrder.WorkoutId };
            workout.Sessions = new List<WorkoutSession>();

            foreach (var id in workoutSessionOrder.OrderedWorkoutSessionList)
            {
                workout.Sessions.Add(new WorkoutSession { Id = id, Order = order++});
            }

            try
            {
                ServiceFactory.Workout.UpdateSessionOrderOnly(workout);
            }
            catch (DataNotFoundException)
            {
                throw new HttpException((int)HttpStatusCode.Forbidden,"Cannot update the session");
                //return Json(new {Status="Data Not Found Error"});
            }
            return Json(new { Status="Saved" });
        }
    }
}
