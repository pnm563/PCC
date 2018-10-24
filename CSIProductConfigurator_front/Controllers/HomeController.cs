﻿using AutomationCommon.Helper;
using AutomationCommon.Helpers;
using AutomationCommon.Model.OAUTH;
using AutomationCommon.Model.WebAPI;
using AutomationCommon.Security;
using CSIProductConfigurationCommon.Enums;
using CSIProductConfigurationCommon.Models;
using CSIProductConfigurator_front.Controllers.Overrides;
using CSIProductConfigurator_front.Data.Configuration;
using CSIProductConfigurator_front.Data.URIs;
using CSIProductConfigurator_front.Helpers;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CSIProductConfigurator_front.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Fetch all configuration types from the back end


            List<ConfigurationType> cTypes = new List<ConfigurationType>();

            using (HttpClient client = NetworkHelper.GetHttpClient(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI], ""))
            {

                HttpResponseMessage response = client.GetAsync(String.Format(ServiceGatewayURI.ConfigurationTypeURI)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response != null)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                cTypes = response.Content.ReadAsAsync<List<ConfigurationType>>().Result;
                            }
                        }
                    }
                }
            }

            //need a try catch in here, expections are just splatted to the user with the red/beige screen :-/

            // Add both of the above to the ConfigurationView view model object
            ConfigurationView newView = new ConfigurationView()
            {
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
            }
            catch
            {
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

        public ActionResult ConfigCalc(ConfigurationView cView)
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
                //CustomerCode = cView.SelectedCustomerCode,
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

            foreach (ConfigurationTypeOutput output in configurationDetail.ConfigurationTypeOutputs)
            {
                if (output.ValueType == AttributeType.FloatType)
                {
                    if (output.Value != null)
                    {
                        output.Value = String.Format("£{0:n}", float.Parse(output.Value));
                    }
                    else
                    {
                        return new JsonHttpStatusResult(configurationDetail.OutputText, HttpStatusCode.InternalServerError);
                    }

                }
            }

            string outputValues = JsonConvert.SerializeObject(configurationDetail);

            //return View(configurationDetail);
            //return new HttpResponseMessage() { Content = new StringContent(configurationDetail.OutputText, Encoding.UTF8, "text/html") };
            return Content(outputValues);
        }

        [HttpGet]
        public ActionResult PreFilledFormTest()
        {
            ServiceRequest serviceRequest = new ServiceRequest(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI]);
            ConfigurationDetail configurationDetail = new ConfigurationDetail();

            configurationDetail = serviceRequest.ExecuteRequest<ConfigurationDetail>(HttpRequestMethod.GET,
                String.Format(
                ServiceGatewayURI.ConfigurationDetailURI)
                );

            return View(configurationDetail);
        }

        public ActionResult Customers()
        {
            List<Customer> theCustomers = new List<Customer>();

            if (!Helper.CheckSessionOAUTHToken((OAUTHtoken)this.Session["OAUTHtoken"]))
            {
                this.Session["OAUTHtoken"] = Helper.GetOAUTHToken();
            }

            OAUTHtoken token = (OAUTHtoken)this.Session["OAUTHtoken"];

            using (HttpClient client = NetworkHelper.GetHttpClient(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI], token.access_token))
            {
                
                HttpResponseMessage response = client.GetAsync(String.Format(ServiceGatewayURI.CustomerURI)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response != null)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                theCustomers = response.Content.ReadAsAsync<List<Customer>>().Result;
                            }
                        }
                    }
                }
            }

            TypeAheadResults<Customer> container = new TypeAheadResults<Customer>();

            container.data.TheResults = theCustomers;

            return Content(JsonConvert.SerializeObject(container));
        }
    }
}