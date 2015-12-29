using System;
using System.Web;
using System.Web.Mvc;

namespace TaskTimer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AllowCrossSiteJsonAttribute());
        }
    }

    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            base.OnActionExecuting(filterContext);
        }
    }

    public class AllowJsonGetAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var jsonResult = filterContext.Result as JsonResult;

            if (jsonResult == null)
                throw new ArgumentException("Action does not return a JsonResult, attribute AllowJsonGet is not allowed");

            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            base.OnResultExecuting(filterContext);
        }
    }
}
