using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FASM.Action_Filters
{
    public class FASMAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (skipAuthorization)
            {
                return;
            }
            //if (filterContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //    {
            //        filterContext.HttpContext.Response.StatusCode = 401;
            //        filterContext.Result = new HttpStatusCodeResult(401, "Please login to continue");
            //        filterContext.HttpContext.Response.End();
            //        //FormsAuthentication.SignOut();
            //    }
            //}

            if (HttpContext.Current.Session["user"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Login" }, { "controller", "Accounts" } });
            }
            else
            {
                //Create permission string based on the requested controller name and action name in the format 'controllername-action'
                string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);

                //Create an instance of our custom user authorization object passing requesting user's 'Windows Username' into constructor
                FASMUser requestingUser = new FASMUser(HttpContext.Current.Session["user"].ToString());
                //Check if the requesting user has the permission to run the controller's action
                if (!(requestingUser.HasPermission(requiredPermission))) //& !requestingUser.IsSysAdmin
                {

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Unauthorized" }});
                }
                //If the user has the permission to run the controller's action, then filterContext.Result will be uninitialized and
                //executing the controller's action is dependant on whether filterContext.Result is uninitialized.
            }
        }
    }
}