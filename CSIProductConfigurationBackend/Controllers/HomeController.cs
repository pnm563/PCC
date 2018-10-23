using AutomationCommon.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSIProductConfigurationBackend.Controllers
{
    [OAUTHattribute]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
