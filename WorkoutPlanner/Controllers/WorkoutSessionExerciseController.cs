using System;
using System.Collections.Generic;
using System.Linq;
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
using Services.Base;
using Shared;
using ViewModels;
using ViewModels.Selectors.Implementations;
using WorkoutPlanner.Controllers.Base;

namespace WorkoutPlanner.Controllers
{
    public class WorkoutSessionExerciseController : BaseController<WorkoutSessionExercise, WorkoutSessionExerciseViewModel>
    {
        public WorkoutSessionExerciseController(IServiceFactory serviceFactory
                                                , IMapperFactory mapperFactory
                                                , IUserProvider userProvider
                                                , ISessionHandler sessionHandler)
            : base(serviceFactory, mapperFactory, userProvider, sessionHandler)
        {
        }

        public WorkoutSessionExerciseViewModel GetViewModel(WorkoutSessionExercise modelToChangeIntoViewModel)
        {
            var allExercices = ServiceFactory.Exercise.GetAll();
            var vm = MapperFactory.WorkoutSessionExercise.GetViewModel(modelToChangeIntoViewModel);
            vm.ListExercise = MapperFactory.Map<IEnumerable<Exercise>, IEnumerable<ExerciseSelector>>(allExercices);
            var exercise = vm.ListExercise.FirstOrDefault(d => Convert.ToInt32(d.Value) == vm.ExerciseId);
            if (exercise!=null)
                exercise.IsSelected = true;
            return vm;
        }

        public ActionResult IndexForWorkoutSession(int workoutSessionId)
        {
            WorkoutSessionViewModel x = ServiceFactory.WorkoutSessionExercise.GetWorkoutSessionWithWorkoutSessionExercise(workoutSessionId);
            return View("Index", x);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var x = ServiceFactory.WorkoutSessionExercise.Get(new WorkoutSessionExercise {Id = id});
            var vm = GetViewModel(x);
            return View("Details", vm);
        }

        [HttpGet]
        public ActionResult CreateForWorkoutSesssion(int idWorkoutSession)
        {
            var x = ServiceFactory.WorkoutSessionExercise.New();
            x.WorkoutSession = new WorkoutSession {Id = idWorkoutSession};
            var vm = GetViewModel(x);
            return View("CreateForWorkoutSession", vm);
        }


        [HttpPost]
        public ActionResult Create(WorkoutSessionExerciseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ServiceFactory.WorkoutSessionExercise.Create(Model);
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }
            WorkoutSessionViewModel x = ServiceFactory.WorkoutSessionExercise.GetWorkoutSessionWithWorkoutSessionExercise(Model.WorkoutSession.Id);
            return View("Index", x);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var x = ServiceFactory.WorkoutSessionExercise.Get(new WorkoutSessionExercise {Id = id});
            var viewModel = MapperFactory.WorkoutSessionExercise.GetViewModel(x);
            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(WorkoutSessionExerciseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ServiceFactory.WorkoutSessionExercise.Update(Model);
            }
            return RedirectToAction("Edit", new { id = Model.Id });
           
        }

        //[HttpGet]
        //public ActionResult GetWorkoutSessionExercisePartial(int? idSessionExercise)
        //{
        //    WorkoutSessionExerciseViewModel viewModel;
        //    if (idSessionExercise.HasValue)
        //    {
        //        viewModel = ServiceFactory.WorkoutSessionExercise.Get(new WorkoutSessionExercise { Id = idSessionExercise.Value });
        //    }
        //    else
        //    {
        //        viewModel = ServiceFactory.WorkoutSessionExercise.New((new WorkoutSessionExercise()));
        //    }
        //    return PartialView("_WorkoutSessionExercisePartial", viewModel);
        //}
        [HttpPost]
        public JsonResult GetWorkoutSessionExercisePartial(int? idSessionExercise, int idExercise, int workoutSessionId)
        {
            WorkoutSessionExercise modelToBind;
            if (idSessionExercise.HasValue)
            {
                modelToBind = ServiceFactory.WorkoutSessionExercise.Get(new WorkoutSessionExercise {Id = idSessionExercise.Value});
            }
            else
            {
                modelToBind = ServiceFactory.WorkoutSessionExercise.Create(new WorkoutSessionExercise { Exercise = new Exercise { Id = idExercise }, WorkoutSession = new WorkoutSession { Id = workoutSessionId } });
            }
            var viewModel = MapperFactory.WorkoutSessionExercise.GetViewModel(modelToBind);
            var ajaxString = RenderPartialView("_WorkoutSessionExercisePartial", viewModel);
            return Json(new { Content = ajaxString, IsNew = !idSessionExercise.HasValue, IdWorkoutSessionExercise = viewModel.Id});
        }

        [HttpPost]
        public JsonResult SaveWorkoutSessionExercisePartial(WorkoutSessionExerciseViewModel viewModel)
        {

            try
            {
                ServiceFactory.WorkoutSessionExercise.UpdatePartial(Model); 
                return Json(new { Status = "Saved" });
            }
            catch (DataNotFoundException)
            {
                throw new HttpException((int)HttpStatusCode.Forbidden, "Cannot update the exercise session");
            }
            
           
        }
    }
}