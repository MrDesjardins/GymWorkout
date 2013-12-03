using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using ViewModels.Selectors.Base;

namespace WorkoutPlanner.Extensions
{
    public class HelperExtensionFactory<TModel> : HelperExtensionFactory
    {

        private HtmlHelper<TModel> HtmlHelper { get; set; }

        public HelperExtensionFactory(HtmlHelper<TModel> htmlHelper)
            : base(htmlHelper)
        {
            HtmlHelper = htmlHelper;
        }

        public MvcHtmlString SelectorFor<TValue>(Expression<Func<TModel, TValue>> property, IEnumerable<ISelector> items)
        {
            var meta = ModelMetadata.FromLambdaExpression(property, this.HtmlHelper.ViewData);
            string fullPropertyName = HtmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(property));

            var selectBuilder = new TagBuilder("select");
            selectBuilder.MergeAttribute("name", fullPropertyName);
            selectBuilder.MergeAttribute("id", fullPropertyName);
            selectBuilder.MergeAttribute("class", "selector");

            items = items.OrderBy(c => c.DisplayText);

            foreach (var item in items)
            {
                var optionBuilder = new TagBuilder("option");
                optionBuilder.MergeAttribute("value", item.Value);
                if (item.IsSelected)
                {
                    optionBuilder.MergeAttribute("selected", "selected");
                }
                optionBuilder.SetInnerText(item.DisplayText);
                selectBuilder.InnerHtml += optionBuilder.ToString();
            }
            return new MvcHtmlString(selectBuilder.ToString());
        }


    }

    public class HelperExtensionFactory
    {
        public HelperExtensionFactory(HtmlHelper htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }

        private HtmlHelper HtmlHelper { get; set; }





        public MvcHtmlString ActionImage(string controller, string action, object routeValues, string imagePath, string alternateText = "", object htmlAttributes = null)
        {

            var anchorBuilder = new TagBuilder("a");
            var url = new UrlHelper(HtmlHelper.ViewContext.RequestContext);

            anchorBuilder.MergeAttribute("href",url.Action(action,controller,routeValues));
            
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alternateText);
            imgBuilder.MergeAttribute("title", alternateText);

            var attributes = (IDictionary<string, object>) HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            imgBuilder.MergeAttributes(attributes);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            anchorBuilder.InnerHtml = imgHtml;
            return MvcHtmlString.Create(anchorBuilder.ToString());
        }


        public MvcHtmlString ActionLink(string linkText
                                        , string actionName = null
                                        , string controllerName = null
                                        , object routeValues = null
                                        , object htmlAttributes=null)
        {
            if (actionName == null)
            {
                actionName = HtmlHelper.ViewContext.RouteData.GetRequiredString("action");
            }
            if (controllerName == null)
            {
                controllerName = HtmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            }
            var routeValues2 = new RouteValueDictionary(routeValues);
            var attributes = (IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (string.IsNullOrEmpty(linkText))
                throw new ArgumentException("linkText");
            else
            {
                var attributeHelper = new AttributeHelper(HtmlHelper);
                var att = attributeHelper.GetAttributes(actionName, controllerName).OfType<AuthorizeAttribute>();
                var isInRole = att.Aggregate(false, (f, g) => f | HtmlHelper.ViewContext.HttpContext.User.IsInRole(g.Roles));
                if (isInRole)
                {
                    return MvcHtmlString.Create(HtmlHelper.GenerateLink(HtmlHelper.ViewContext.RequestContext, HtmlHelper.RouteCollection, linkText, (string)null, actionName, controllerName, routeValues2, attributes));
                }
                return new MvcHtmlString("");
            }
        }

      


    

    }
}