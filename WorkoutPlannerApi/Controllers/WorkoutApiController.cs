using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mappers;
using Mappers.Factory;
using Model;
using Services;
using Services.Base;
using ViewModels;

namespace WorkoutPlannerApi.Controllers
{
    public class WorkoutApiController : BaseApiController
    {

        public WorkoutApiController(IServiceFactory serviceFactory, IMapperFactory mapperFactory):base(serviceFactory, mapperFactory)
        {
        }

        // GET /api/Workout/GetWorkouts
        public IEnumerable<WorkoutViewModel> GetWorkouts()
        {
            var x = ServiceFactory.Workout.GetAll();
            var vm = MapperFactory.Workout.GetViewModelList(x);
            return vm;
        }

        // GET api/values/5
        public WorkoutViewModel GetWorkout(int id)
        {
            var x = ServiceFactory.Workout.Get(new Workout { Id = id });
            //throw new HttpResponseException(HttpStatusCode.NotFound);
            var vm = MapperFactory.Workout.GetViewModel(x);
            return vm;
        }

        // POST api/values
        public HttpResponseMessage PostWorkout([FromBody]WorkoutViewModel viewModel)
        {
            var model = MapperFactory.Workout.GetModel(viewModel);
            var modelCreated = ServiceFactory.Workout.Create(model);
            var viewModelCreated = MapperFactory.Workout.GetViewModel(modelCreated);
            var response = Request.CreateResponse<WorkoutViewModel>(HttpStatusCode.Created, viewModelCreated);
            string uri = Url.Link("DefaultApi", new { id = viewModel.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT api/values/5
        public void PutWorkout(int id, [FromBody]WorkoutViewModel viewModel)
        {
            viewModel.Id = id;
            var model = MapperFactory.Workout.GetModel(viewModel);
            if (ServiceFactory.Workout.Update(model) == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE api/values/5
        public void DeleteWorkout(int id)
        {
            var count = ServiceFactory.Workout.Delete(new Workout { Id=id});
            if (count==0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}