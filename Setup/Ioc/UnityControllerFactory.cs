using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace WorkoutPlanner.Ioc
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            IController controller;
            if (controllerType == null)
            {
                throw new HttpException(404, String.Format("The controller for path '{0}' could not be found or it does not implement IController.", requestContext.HttpContext.Request.Path));
            }
            if (!typeof (IController).IsAssignableFrom(controllerType))
            {
                throw new ArgumentException(string.Format("Type requested is not a controller: {0}", controllerType.Name), "controllerType");
            }
            try
            {
                 var resolvedEntity = MvcUnityContainer.Container.Resolve(controllerType);
                 controller = resolvedEntity as IController;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(String.Format("Error resolving controller {0}", controllerType.Name), ex);
            }
            return controller;
        }

        #region Nested type: MvcUnityContainer

        public static class MvcUnityContainer
        {
            public static IUnityContainer Container { get; set; }
        }

        #endregion
    }
}