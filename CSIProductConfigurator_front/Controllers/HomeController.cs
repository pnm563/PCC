using AutomationCommon.Helper;
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

            #region Fetch OAUTH token
            if (!Helper.CheckSessionOAUTHToken((OAUTHtoken)this.Session["OAUTHtoken"]))
            {
                this.Session["OAUTHtoken"] = Helper.GetOAUTHToken();
            }
            
            OAUTHtoken token = (OAUTHtoken)this.Session["OAUTHtoken"];
            #endregion

            List<ConfigurationType> cTypes = new List<ConfigurationType>();

            #region Fetch list of configuration types
            using (HttpClient client = NetworkHelper.GetHttpClient(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI], token.access_token))
            {

                HttpResponseMessage response = client.GetAsync(String.Format(ServiceGatewayURI.ConfigurationTypeURI)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            cTypes = response.Content.ReadAsAsync<List<ConfigurationType>>().Result;
                        }
                    }
                }
            }
            #endregion

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

            #region Get OAUTH token
            if (!Helper.CheckSessionOAUTHToken((OAUTHtoken)this.Session["OAUTHtoken"]))
            {
                this.Session["OAUTHtoken"] = Helper.GetOAUTHToken();
            }

            OAUTHtoken token = (OAUTHtoken)this.Session["OAUTHtoken"];
            #endregion

            List<ConfigurationTypeParameter> cTypeParams = new List<ConfigurationTypeParameter>();
            List<ParameterValue> paramVals = new List<ParameterValue>();

            using (HttpClient client = NetworkHelper.GetHttpClient(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI], token.access_token))
            {
                #region GetConfigurationTypeParameters
                // Fetch and populate cTypeParams with all the parameters relevant to the configuration type passed into this method via id
                HttpResponseMessage response = client.GetAsync(String.Format(ServiceGatewayURI.GetConfigurationTypeParameterByConfigurationTypeIDURI, id)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            cTypeParams = response.Content.ReadAsAsync<List<ConfigurationTypeParameter>>().Result;
                        }
                        else
                        {
                            return new HttpStatusCodeResult(response.StatusCode);
                        }
                    }
                }
                #endregion

                #region GetParamterValues
                // Fetch all fixed parameter values ready to populate dropdown boxes with these choices.
                response = client.GetAsync(String.Format(ServiceGatewayURI.ParameterValueURI)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            paramVals = response.Content.ReadAsAsync<List<ParameterValue>>().Result;
                        }
                        else
                        {
                            return new HttpStatusCodeResult(response.StatusCode);
                        }
                    }
                }
                #endregion
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
            #region fetch OAUTHtoken and output type list
            if (!Helper.CheckSessionOAUTHToken((OAUTHtoken)this.Session["OAUTHtoken"]))
            {
                this.Session["OAUTHtoken"] = Helper.GetOAUTHToken();
            }

            OAUTHtoken token = (OAUTHtoken)this.Session["OAUTHtoken"];

            List<ConfigurationTypeOutput> cTypeOutputs = new List<ConfigurationTypeOutput>();

            using (HttpClient client = NetworkHelper.GetHttpClient(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI], token.access_token))
            {
                HttpResponseMessage response = client.GetAsync(String.Format(ServiceGatewayURI.GetConfigurationTypeOutputByConfigurationTypeIDURI,id)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            cTypeOutputs = response.Content.ReadAsAsync<List<ConfigurationTypeOutput>>().Result;
                        }
                    }
                }
            }
            #endregion 
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

            #region Fetch OAUTHtoken
            if (!Helper.CheckSessionOAUTHToken((OAUTHtoken)this.Session["OAUTHtoken"]))
            {
                this.Session["OAUTHtoken"] = Helper.GetOAUTHToken();
            }

            OAUTHtoken token = (OAUTHtoken)this.Session["OAUTHtoken"];
            #endregion

            List<Parameter> theParams = new List<Parameter>();

            #region Fetch list of parameters
            
            using (HttpClient client = NetworkHelper.GetHttpClient(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI], token.access_token))
            {
                HttpResponseMessage response = client.GetAsync(String.Format(ServiceGatewayURI.ParameterURI)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            theParams = response.Content.ReadAsAsync<List<Parameter>>().Result;
                        }
                        else
                        {
                            return new HttpStatusCodeResult(response.StatusCode);
                        }
                    }
                }
            }
            #endregion

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
            #region Fetch OAUTH token, followed by pre filled form test
            if (!Helper.CheckSessionOAUTHToken((OAUTHtoken)this.Session["OAUTHtoken"]))
            {
                this.Session["OAUTHtoken"] = Helper.GetOAUTHToken();
            }

            OAUTHtoken token = (OAUTHtoken)this.Session["OAUTHtoken"];

            ConfigurationDetail configurationDetail = new ConfigurationDetail();

            using (HttpClient client = NetworkHelper.GetHttpClient(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI], token.access_token))
            {

                HttpResponseMessage response = client.GetAsync(String.Format(ServiceGatewayURI.ConfigurationDetailURI)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            configurationDetail = response.Content.ReadAsAsync<ConfigurationDetail>().Result;
                        }
                        else
                        {
                            return new HttpStatusCodeResult(response.StatusCode);
                        }
                    }
                }
            }
            #endregion

            return View(configurationDetail);
        }

        public ActionResult Customers()
        {
            #region Fetch OAUTH token, followed by customer list
            if (!Helper.CheckSessionOAUTHToken((OAUTHtoken)this.Session["OAUTHtoken"]))
            {
                this.Session["OAUTHtoken"] = Helper.GetOAUTHToken();
            }

            OAUTHtoken token = (OAUTHtoken)this.Session["OAUTHtoken"];

            List<Customer> theCustomers = new List<Customer>();

            using (HttpClient client = NetworkHelper.GetHttpClient(ConfigurationManager.AppSettings[ConfigurationParams.ServiceGatewayURI], token.access_token))
            {
                
                HttpResponseMessage response = client.GetAsync(String.Format(ServiceGatewayURI.CustomerURI)).Result;
                if (response != null)
                {
                    using (response)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            theCustomers = response.Content.ReadAsAsync<List<Customer>>().Result;
                        }
                        else
                        {
                            return new HttpStatusCodeResult(response.StatusCode);
                        }
                    }
                }
            }
            #endregion

            TypeAheadResults<Customer> container = new TypeAheadResults<Customer>();

            container.data.TheResults = theCustomers;

            return Content(JsonConvert.SerializeObject(container));
        }
    }
}