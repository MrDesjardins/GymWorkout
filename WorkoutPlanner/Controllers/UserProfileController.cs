using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Sessions;
using Mappers.Factory;
using Model;
using Services.Base;
using ViewModels;
using ViewModels.Selectors.Implementations;
using WorkoutPlanner.Controllers.Base;

namespace WorkoutPlanner.Controllers
{
    /*
    public class UserProfileController : BaseController<ApplicationUser, UserProfileViewModel>
    {
        //
        // GET: /ApplicationUser/

        public UserProfileController(IServiceFactory serviceFactory
                                , IMapperFactory mapperFactory
                                , IUserProvider userProvider
                                , ISessionHandler sessionHandler) : base(serviceFactory, mapperFactory, userProvider, sessionHandler)
        {
        }



        public ActionResult Edit()
        {
            var m = ServiceFactory.Account.Get(new ApplicationUser() { UserId = CurrentUser.UserId });
            var vm = MapperFactory.ApplicationUser.GetViewModel(m);
            vm.Languages = new List<LanguageSelector> { new LanguageSelector("fr-CA", "Français"), new LanguageSelector("en-US", "English") };
            foreach (var lang in vm.Languages)
            {
                if (lang.Value == m.Language)
                {
                    lang.IsSelected = true;
                }
            }
            return View("Edit", vm);
        }


        [HttpPost]
        public ActionResult Edit(UserProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ServiceFactory.Account.Update(Model);
            }

            return RedirectToAction("Edit");
        }

    }
     * */
}
