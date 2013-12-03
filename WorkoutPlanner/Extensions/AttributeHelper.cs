using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WorkoutPlanner.Extensions
{
    public class AttributeHelper
    {
        private readonly HtmlHelper _htmlHelper;
        public AttributeHelper(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public IEnumerable<Attribute> GetAttributes(
                        string actionName,
                        string controllerName,
                        string method = "GET")
        {
            var controllerFactory = ControllerBuilder.Current.GetControllerFactory();
            var otherController = (ControllerBase)controllerFactory.CreateController(new RequestContext(_htmlHelper.ViewContext.HttpContext, new RouteData()), controllerName);
            var controllerDescriptor = new ReflectedControllerDescriptor(otherController.GetType());
            var controllerContext2 = new ControllerContext(new HttpContextWrapperWithHttpMethod(_htmlHelper.ViewContext.HttpContext.ApplicationInstance.Context, method),
                     new RouteData(), otherController);
            var actionDescriptor = controllerDescriptor.FindAction(controllerContext2, actionName);
            var attributes = actionDescriptor.GetCustomAttributes(true).Cast<Attribute>().ToArray();
            return attributes;
        }

        private class HttpContextWrapperWithHttpMethod : HttpContextWrapper
        {
            private readonly HttpRequestBase _request;

            public HttpContextWrapperWithHttpMethod(HttpContext httpContext, string method): base(httpContext)
            {
                this._request = new HttpRequestWrapperWithHttpMethod(httpContext.Request, method);
            }

            public override HttpRequestBase Request
            {
                get { return _request; }
            }

            private class HttpRequestWrapperWithHttpMethod : HttpRequestWrapper
            {
                private readonly string _httpMethod;

                public HttpRequestWrapperWithHttpMethod(HttpRequest httpRequest, string httpMethod): base(httpRequest)
                {
                    this._httpMethod = httpMethod;
                }

                public override string HttpMethod
                {
                    get { return _httpMethod; }
                }
            }
        }

    }
}