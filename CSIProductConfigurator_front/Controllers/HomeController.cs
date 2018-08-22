using CSIProductConfigurationCommon.Enums;
using CSIProductConfigurationCommon.Models;
using CSIProductConfigurator_front.Data.Configuration;
using CSIProductConfigurator_front.Data.URIs;
using CSIProductConfigurator_front.Models;
using Newtonsoft.Json;
using Niu.OneWorkspace.Common.Enums;
using Niu.OneWorkspace.Common.ServiceEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CSIProductConfigurator_front.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Create a dummy list of customer codes
            List<String> customerCodes = new List<String> { "cust001", "cust002", "Cust003" };

            // Fetch all configuration types from the back end
            ServiceRequest serviceRequest = new ServiceRequest(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI]);
            List<ConfigurationType> cTypes = serviceRequest.ExecuteRequest<List<ConfigurationType>>(HttpRequestMethod.GET,
                String.Format(
                    ServiceGatewayURI.ConfigurationTypeURI)
            );

            // Add both of the above to the ConfigurationView view model object
            ConfigurationView newView = new ConfigurationView()
            {
                CustomerCodes = customerCodes,
                ConfigurationTypes = cTypes
            };

            // Return the view with view model passed in. Added collections above will populate the first two drop down boxes
            return View(newView);
        }

        public ActionResult ConfigurationTypeParameterList(String id)
        {

            //Returns a partial view with all dynamic parameters for selected configuration type
            //A <div> is populated via a .load Ajax call to this controller method


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Also handle if id not found

            ServiceRequest serviceRequest = new ServiceRequest(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI]);

            List<ConfigurationTypeParameter> cTypeParams = new List<ConfigurationTypeParameter>();
            List<ParameterValue> paramVals = new List<ParameterValue>();


            // Fetch and populate cTypeParams with all the parameters relevant to the configuration type passed into this method via id
            try
            {
                cTypeParams = serviceRequest.ExecuteRequest<List<ConfigurationTypeParameter>>(HttpRequestMethod.GET,
                    String.Format(
                        ServiceGatewayURI.GetConfigurationTypeParameterByConfigurationTypeIDURI, id)
                );
            } catch {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            // Fetch all fixed parameter values ready to populate dropdown boxes with these choices.
            try
            {
                paramVals = serviceRequest.ExecuteRequest<List<ParameterValue>>(HttpRequestMethod.GET,
                    String.Format(
                        ServiceGatewayURI.ParameterValueURI)
                );
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            // Nested loop round each configuration type, adding fixed parameter values to the list when there is a match from the ParameterValues collection
            

            foreach (ConfigurationTypeParameter ctp in cTypeParams)
            {
                ctp.ParameterValues = new List<ParameterValue>();

                foreach (ParameterValue pv in paramVals)
                {

                    if (pv.ParameterID == ctp.ParameterID)
                    {
                        ctp.ParameterValues.Add(pv);
                    }
                }
            }


            return PartialView("_ConfigurationTypeParameterList", cTypeParams);
        }

        public ActionResult ConfigurationTypeOutputList(String id)
        {
            ServiceRequest serviceRequest = new ServiceRequest(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI]);
            List<ConfigurationTypeOutput> cTypeOutputs = new List<ConfigurationTypeOutput>();

            try
            {
                cTypeOutputs = serviceRequest.ExecuteRequest<List<ConfigurationTypeOutput>>(HttpRequestMethod.GET,
                    String.Format(
                        ServiceGatewayURI.GetConfigurationTypeOutputByConfigurationTypeIDURI, id)
                );
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return PartialView("_ConfigurationTypeOutputList", cTypeOutputs);
        }

        public ActionResult Regarder()
        {
           
            return View();
        }
        [HttpPost]

        public ActionResult Contact(ConfigurationView cView)
        {
            //Add bind includes
            //Add validate anti forgery
            //Add Is Model Valid?

            ServiceRequest serviceRequest = new ServiceRequest(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI]);

            List<Parameter> theParams = new List<Parameter>();

            try
            {
                theParams = serviceRequest.ExecuteRequest<List<Parameter>>(HttpRequestMethod.GET,
                    String.Format(
                        ServiceGatewayURI.ParameterURI)
                );
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ConfigurationDetail configurationDetail = new ConfigurationDetail()
            {
                CustomerCode = cView.SelectedCustomerCode,
                ConfigurationTypeID = cView.SelectedConfigurationType,
                Name = cView.Name,
                Description = cView.Description,
                Id = Guid.NewGuid().ToString(),
                ConfigurationParameterValues = new List<ConfigurationParameterValue>()
            };

            foreach (String pipyString in cView.ListOfConfigurationParameterValues)
            {
                
                String id = pipyString.Split('|')[0];
                String value = pipyString.Split('|')[1];
                String parameterName = null;
                AttributeType parameterType = 0;


                foreach (Parameter param in theParams)
                {
                    if (param.Id.ToString() == id)
                    {
                        parameterName = param.Name;
                        parameterType = param.ParameterType1;
                    }
                }

                configurationDetail.ConfigurationParameterValues.Add(new ConfigurationParameterValue() { ConfigurationID = configurationDetail.Id, ParameterID = id, ParameterName = parameterName, ParameterType1 = parameterType, Value = value });

            }



            string JSONdocType = JsonConvert.SerializeObject(configurationDetail);

            HttpContent content = new StringContent(JSONdocType, Encoding.UTF8, "application/json");

            configurationDetail = serviceRequest.ExecuteRequest<ConfigurationDetail>(HttpRequestMethod.POST,
                String.Format(
                ServiceGatewayURI.ConfigurationDetailURI),
                content
                );

            ViewBag.Message = "Your contact page.";

            return View(configurationDetail);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ServiceRequest serviceRequest = new ServiceRequest(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI]);
            ConfigurationDetail configurationDetail = new ConfigurationDetail();

            configurationDetail = serviceRequest.ExecuteRequest<ConfigurationDetail>(HttpRequestMethod.GET,
                String.Format(
                ServiceGatewayURI.ConfigurationDetailURI)
                );

            return View(configurationDetail);
        }

    }
}