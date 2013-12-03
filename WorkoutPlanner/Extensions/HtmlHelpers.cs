using System.Web;
using System.Web.Mvc;


namespace WorkoutPlanner.Extensions
{
    public static class HtmlHelpers
    {


        public static HelperExtensionFactory Input(this HtmlHelper helper)
        {
            return new HelperExtensionFactory(helper);
        }


        public static HelperExtensionFactory<TModel> Input<TModel>(this HtmlHelper<TModel> helper)
        {
            return new HelperExtensionFactory<TModel>(helper);
        }

    }
}