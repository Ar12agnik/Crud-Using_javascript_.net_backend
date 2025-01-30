using System;
using System.Web;
using System.Web.Mvc;

public class CorsFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://127.0.0.1:5500");
        filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        filterContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");

        if (filterContext.HttpContext.Request.HttpMethod == "OPTIONS")
        {
            filterContext.HttpContext.Response.StatusCode = 200;
            filterContext.HttpContext.Response.End();
        }

        base.OnActionExecuting(filterContext);
    }
}
