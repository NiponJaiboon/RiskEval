using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BBAdminWeb.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session.IsNewSession || session["Session"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary
                                            {
                                                { "Controller", "Login" },
                                                { "Action", "LogOut" }
                                            });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}