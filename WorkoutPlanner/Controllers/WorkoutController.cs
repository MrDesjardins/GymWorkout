using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WorkoutPlanner.Controllers
{
    public class WorkoutController : BaseController<Workout, WorkoutViewModel>
    {
        public WorkoutController(IServiceFactory serviceFactory
                                 , IMapperFactory mapperFactory
                                 , IUserProvider userProvider
                                 , ISessionHandler sessionHandler) : base(serviceFactory, mapperFactory, userProvider, sessionHandler)
        {
        }


        public ActionResult Index()
        {
            var modelFromService = ServiceFactory.Workout.GetAll();
            var x = MapperFactory.Map<IEnumerable<Workout>, IEnumerable<WorkoutViewModel>>(modelFromService);
            return View("Index",x);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var x = ServiceFactory.Workout.Get(new Workout { Id = id });
            var viewModelToBind = MapperFactory.Map<Workout, WorkoutViewModel>(x);
            return View("Details", viewModelToBind);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(WorkoutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ServiceFactory.Workout.Create(Model);
                    return Index();
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }
            return View("Create");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var modelToBind = ServiceFactory.Workout.Get(new Workout { Id = id });
            var viewModelToBind = MapperFactory.Map<Workout, WorkoutViewModel>(modelToBind);
            return View("Edit", viewModelToBind);
        }



        [HttpPost]
        public ActionResult Edit(WorkoutViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var numberUpdated = ServiceFactory.Workout.Update(Model);
                if (numberUpdated > 0)
                {
                    viewModel.SavedMessage = "Workout Saved Successfully";
                }
            }

            return View("Edit", viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ServiceFactory.Workout.Delete(new Workout { Id = id });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditWithSessionEdit(int idWorkout)
        {
            var viewModel = BuildWorkoutWithAllExercises(idWorkout);
            return View("EditWithSessionEdit", viewModel);
        }

        private WorkoutViewModel BuildWorkoutWithAllExercises(int idWorkout)
        {
            var modelToBind = ServiceFactory.Workout.Get(new Workout { Id = idWorkout });
            var allExercisesModelAvailable = ServiceFactory.Exercise.GetAll();
            var allExercisesAvailable = MapperFactory.GetMapper<Exercise, ExerciseViewModel>().GetViewModelList(allExercisesModelAvailable);

            var viewModel = MapperFactory.GetMapper<Workout, WorkoutViewModel>().GetViewModel(modelToBind);
            viewModel.AvailablesExercise = allExercisesAvailable;
            return viewModel;
        }

        [HttpPost]
        public ActionResult EditWithSessionEdit(WorkoutViewModel viewModel)
        {
            dynamic json = JsonConvert.DeserializeObject(viewModel.SessionsString);

            Model.Sessions = new Collection<WorkoutSession>();
            foreach (var session in json.sessions)
            {
                var workoutSession = new WorkoutSession { Id = session.id };
                Model.Sessions.Add(workoutSession);
                workoutSession.WorkoutSessionExercises = new Collection<WorkoutSessionExercise>();
                foreach (var exercise in session.exercises) 
                {
                    var workoutSessionExercise = new WorkoutSessionExercise { Exercise = new Exercise { Id = exercise.idexercise } };
                    if (exercise.idsessionexercise != "")
                    {
                        workoutSessionExercise.Id = exercise.idsessionexercise;
                    }
                    workoutSession.WorkoutSessionExercises.Add(workoutSessionExercise);
                }
            }
            int numberUpdated =ServiceFactory.Workout.Update(Model);
            
            var viewModelReturn = BuildWorkoutWithAllExercises(Model.Id);

            if (numberUpdated > 0)
            {
                viewModelReturn.SavedMessage = "Workout Saved Successfully";
            }
            return View("EditWithSessionEdit", viewModelReturn);
        }



    }
}