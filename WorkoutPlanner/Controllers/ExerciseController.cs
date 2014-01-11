using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Sessions;
using BusinessLogic.Validations;
using DataAccessLayer;
using Mappers;
using Mappers.Factory;
using Model;
using Services.Base;
using ViewModels;
using ViewModels.Selectors.Implementations;
using WorkoutPlanner.Controllers.Base;

namespace WorkoutPlanner.Controllers
{
    public class ExerciseController : BaseController<Exercise, ExerciseViewModel>
    {
        public ExerciseController(IServiceFactory serviceFactory
                                  , IMapperFactory mapperFactory
                                  , IUserProvider userProvider
                                  , ISessionHandler sessionHandler)
            : base(serviceFactory, mapperFactory, userProvider, sessionHandler)
        {
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var x = ServiceFactory.Exercise.GetAll();
            var vm = MapperFactory.GetMapper<Exercise, ExerciseViewModel>().GetViewModelList(x);
            return View(vm);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var x = ServiceFactory.Exercise.Get(new Exercise { Id = id });
            var allMuscles = ServiceFactory.Muscle.GetAll();
            var vm = MapperFactory.GetMapper<Exercise, ExerciseViewModel>().GetViewModel(x);
            vm.ListMuscles = MapperFactory.Map<IEnumerable<Muscle>,IEnumerable<MuscleSelector>>(allMuscles);
            foreach (var allMuscle in vm.ListMuscles.Where(allMuscle => Convert.ToInt32(allMuscle.Value) == x.Muscle.Id))
            {
                allMuscle.IsSelected = true;
            }
            return View("Details", vm);
        }

        [HttpGet]
        [Views.Authorize(Roles = Roles.ADMINISTRATOR)]
        public ActionResult Create()
        {
            var x = ServiceFactory.Exercise.New();
            var vm = MapperFactory.GetMapper<Exercise, ExerciseViewModel>().GetViewModel(x);
            vm.ListMuscles = ServiceFactory.Muscle.GetAllSelector();
            return View("Create",vm);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMINISTRATOR)]
        public ActionResult Create(ExerciseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var viewModelSaved = ServiceFactory.Exercise.Create(Model);
                    return RedirectToAction("Details", new { Id = viewModelSaved.Id });
                }
                catch (ValidationErrors propertyErrors)
                {
                    ModelState.AddValidationErrors(propertyErrors);
                }
            }
            viewModel.ListMuscles = ServiceFactory.Muscle.GetAllSelector();
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var x = ServiceFactory.Exercise.Get(new Exercise { Id = id });
            var vm = MapperFactory.GetMapper<Exercise, ExerciseViewModel>().GetViewModel(x);
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(ExerciseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ServiceFactory.Exercise.Update(Model);
            }
            var x = ServiceFactory.Exercise.Get(Model);
            var viewModelToReturn = MapperFactory.GetMapper<Exercise, ExerciseViewModel>().GetViewModel(x);
            return View("Edit", viewModelToReturn);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ServiceFactory.Exercise.Delete(new Exercise { Id = id });
            return RedirectToAction("Index");
        }
    }
}