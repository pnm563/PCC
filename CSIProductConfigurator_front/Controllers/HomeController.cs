using CSIProductConfigurationCommon.Models;
using CSIProductConfigurator_front.Data.Configuration;
using CSIProductConfigurator_front.Data.URIs;
using CSIProductConfigurator_front.Models;
using Niu.OneWorkspace.Common.Enums;
using Niu.OneWorkspace.Common.ServiceEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSIProductConfigurator_front.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<String> customerCodes = new List<String> { "cust001", "cust002", "Cust003" };

            ServiceRequest serviceRequest = new ServiceRequest(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI]);
            List<ConfigurationType> cTypes = serviceRequest.ExecuteRequest<List<ConfigurationType>>(HttpRequestMethod.GET,
                String.Format(
                    ServiceGatewayURI.ConfigurationTypeURI)
            );

            ConfigurationView newView = new ConfigurationView()
            {
                CustomerCodes = customerCodes,
                ConfigurationTypes = cTypes
            };

            return View(newView);
        }

        public ActionResult ConfigurationTypeParameterList()
        {
            ServiceRequest serviceRequest = new ServiceRequest(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI]);

            List<ConfigurationTypeParameter> types = serviceRequest.ExecuteRequest<List<ConfigurationTypeParameter>>(HttpRequestMethod.GET,
                String.Format(
                    ServiceGatewayURI.ConfigurationTypeParameterURI)
            );

            
            return PartialView("_ConfigurationTypeParameterList", types);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}