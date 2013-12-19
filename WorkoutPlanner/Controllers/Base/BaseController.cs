using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Sessions;
using Mappers;
using Mappers.Factory;
using Model;
using Model.Definitions;
using Services.Base;
using Shared;

namespace WorkoutPlanner.Controllers.Base
{
    public abstract class BaseController<TModel, TViewModel> : Controller
    {
        private readonly IMapperFactory _mapperFactory;
        private readonly IServiceFactory _serviceFactory;
        private readonly ISessionHandler _sessionHandler;
        private readonly IUserProvider _userProvider;


        protected BaseController(IServiceFactory serviceFactory, IMapperFactory mapperFactory, IUserProvider userProvider, ISessionHandler sessionHandler)
        {
            _serviceFactory = serviceFactory;
            _mapperFactory = mapperFactory;
            _userProvider = userProvider;
            _sessionHandler = sessionHandler;
        }

        protected TModel Model { get; private set; }

        protected IServiceFactory ServiceFactory
        {
            get { return _serviceFactory; }
        }

        protected IMapperFactory MapperFactory {
            get { return _mapperFactory; }
        }

        protected ICurrentUser CurrentUser
        {
            get
            {
                UserSessionDTO currentUser = _sessionHandler.GetUser();
                if (currentUser != null)
                {
                    UserProfile userProfile = _mapperFactory.UserSessionDTO.GetModel(currentUser);
                    return userProfile;
                }
                else
                {
                    ICurrentUser currentUserFromProvider = _userProvider.Account;
                    UserProfile fullUserProfile = _serviceFactory.Account.GetByUserName(currentUserFromProvider.UserName);
                    if (fullUserProfile == null)//Case of a non identified user
                    {
                        fullUserProfile = new UserProfile();
                        fullUserProfile.Language =  Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages.First():"en-US";
                    }
                    return fullUserProfile;
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentUser.Language);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentUser.Language);
            base.OnActionExecuting(filterContext);
            if (filterContext.ActionParameters.Any())
            {
                KeyValuePair<string, object> possibleViewModel = filterContext.ActionParameters.FirstOrDefault(x => x.Value != null && x.Value.GetType() == typeof (TViewModel));
                if (possibleViewModel.Value != null)
                {
                    var viewModel = (TViewModel) possibleViewModel.Value;
                    var model = (TModel) Activator.CreateInstance(typeof (TModel));
                    Model = _mapperFactory.Map(viewModel, model);
                    ApplyOwnership();
                    ApplyErrorsToModelState(model, viewModel);
                }
            }
        }

        private void ApplyErrorsToModelState(TModel model, TViewModel viewModel)
        {
        ICollection<ValidationResult> result;
        ValidateDataAnnotation(model, out result);
        foreach (ValidationResult validationResult in result)
        {
            foreach (string memberName in validationResult.MemberNames)
            {
                ModelState.AddModelError(memberName, validationResult.ErrorMessage);
            }
        }

            if (Model is IValidatableObject)
            {
                IEnumerable<ValidationResult> errors = (Model as IValidatableObject).Validate(new ValidationContext(this));
                foreach (ValidationResult validationResult in errors)
                {
                    if (validationResult is EnhancedMappedValidationResult<TModel>)
                    {
                        var enhanced = (EnhancedMappedValidationResult<TModel>)validationResult;
                        var viewModelPropertyName = _mapperFactory.GetMapper(model, viewModel).GetErrorPropertyMappedFor(enhanced.Property);
                        ModelState.AddModelError(viewModelPropertyName, validationResult.ErrorMessage);
                    }
                    else
                    {       
                        
                        if (validationResult.MemberNames.Any())
                        {
                            foreach (string memberName in validationResult.MemberNames)
                            {
                                ModelState.AddModelError(memberName, validationResult.ErrorMessage);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
                        }
                    }
                }
            }
            /*
            //This validate underlying entity which can be not fully loaded in the case of reference
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => Model, Model.GetType());

            foreach (ModelValidationResult validationResult in ModelValidator.GetModelValidator(metadata, this.ControllerContext).Validate(null))
            {
                var propertyName = validationResult.MemberName;
                ModelState.AddModelViewModelToErrorsMap(propertyName, validationResult.Message);
            }*/
        }

        private bool ValidateDataAnnotation(object entity, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(entity);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(entity, context, results, true);
        }
        
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is DataNotFoundException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction("NoAccess", "Error");
            }
            base.OnException(filterContext);
        }


        private void ApplyOwnership()
        {
            if (Model is IUserOwnable)
            {
                (Model as IUserOwnable).UserId = _userProvider.Account.UserId; //Set back the ownership of the entity
            }
        }


        protected string RenderPartialView(string partialViewName, object model)
        {
            if (ControllerContext == null)
                return string.Empty;

            if (model == null)
                throw new ArgumentNullException("model");

            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentNullException("partialViewName");

            ModelState.Clear();//Remove possible model binding error.

            ViewData.Model = model;//Set the model to the partial view

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, partialViewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }
       
    }
}