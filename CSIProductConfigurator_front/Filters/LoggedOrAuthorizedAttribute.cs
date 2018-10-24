using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSIProductConfigurator_front.Filters
{
    public class LoggedOrAuthorized : AuthorizeAttribute
    {
        public LoggedOrAuthorized()
        {
            View = "Unauthorized";
        }

        public String View { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            CheckIfUserIsAuthenticated(filterContext);
        }

        private void CheckIfUserIsAuthenticated(AuthorizationContext filterContext)
        {
            // If Result is null, we're OK: the user is authenticated and authorized. 
            if (filterContext.Result == null)
                return;

            // If here, you're getting an HTTP 401 status code. In particular,
            // filterContext.Result is of HttpUnauthorizedResult type. Check Ajax here. 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (String.IsNullOrEmpty(View))
                    return;
                var result = new ViewResult { ViewName = View };
                result.ViewBag.UnauthorizedMessage = "Please check role membership.";
                filterContext.Result = result;
            }
        }
    }
}