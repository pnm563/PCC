using AutomationCommon;
using AutomationCommon.Helper;
using AutomationCommon.Helpers;
using AutomationCommon.Model.OAUTH;
using AutomationCommon.Model.WebAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace CSIProductConfigurator_front.Helpers
{
    public class Helper
    {
        public static OAUTHtoken GetOAUTHToken()
        {
            Result returnResult = new Result();
            
            Assembly assembly = Assembly.GetExecutingAssembly();
            GuidAttribute attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            String id = attribute.Value;

            X509Certificate2 clientCert = ClientCertificateHelper.GetClientCertificate("RGDAT108VCC");

            OAUTHtoken OAUTHtokenReturned = new OAUTHtoken();

            using (HttpClient quangoPingClient = NetworkHelper.GetHttpClient("https://beta-sso.cognisec.com:9998", new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"), clientCert))   //Get an instance of HttpClient and populate with token and stuff
            {
                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "client_credentials")                                        //Ping expects a Form with client credentials request
                    }
                );
                string quangoPingUri = "/as/token.oauth2?client_id=cc_"+id;                                                                           //Ping OAUTH URI set, ready to pass to HttpClient
                try
                {
                    using (HttpResponseMessage quangoPingResponse = quangoPingClient.PostAsync(quangoPingUri, content).Result)       //Attempt to POST the form and extract Result
                    {
                        if (quangoPingResponse.StatusCode != HttpStatusCode.OK)                                                     //Connected to host, but got non-OK HTTP back
                        {
                            try
                            {
                                OAUTHerror OAUTHerrorReturned = JsonConvert.DeserializeObject<OAUTHerror>(quangoPingResponse.Content.ReadAsStringAsync().Result);   //Attempt to get OAUTHerror
                                returnResult.ExceptionInfo = OAUTHerrorReturned.error_description;
                                returnResult.ResultText = "OAUTH token error";
                            }
                            catch (Exception ex)
                            {
                                returnResult.ExceptionInfo = ex.Message;                                                                                            //No OAUTHerror found
                                returnResult.ResultText = "OAUTH response deserialisation failed - HTTP status code " + quangoPingResponse.StatusCode;              //so just send status code
                            }
                            returnResult.Outcome = "Fail";
                        }
                        else
                        {
                            OAUTHtokenReturned = JsonConvert.DeserializeObject<OAUTHtoken>(quangoPingResponse.Content.ReadAsStringAsync().Result);

                            returnResult.Outcome = "Success";
                        }
                    }
                }
                catch (Exception ex)                                                    //Failed to connect to host                                                                           
                {
                    returnResult.ExceptionInfo = ex.InnerException.InnerException.Message;
                    returnResult.ResultText = "Failed to contact token provider";
                    returnResult.Outcome = "Fail";
                }

                if (returnResult.Outcome.Equals("Fail"))                                //Inform the user of the failure
                {
                    //  result.Text("Outcome " + returnResult.Outcome);
                    //  result.Text("ResultText " + returnResult.ResultText);
                    //  result.Text("ExceptionInfo " + returnResult.ExceptionInfo);
                    string fail = "Outcome " + returnResult.Outcome;
                    fail += "\nResultText " + returnResult.ResultText;
                    fail += "\nExceptionInfo " + returnResult.ExceptionInfo;

                    throw new Exception(fail);
                }
            }
            return OAUTHtokenReturned;
        }

        public static bool CheckSessionOAUTHToken(OAUTHtoken tokenIn)
        {
            if (tokenIn == null)
            {
                return false;
            }

            Result res = OAUTHHelper.CheckAuth(tokenIn.access_token);

            if (res.Outcome.Equals("Success"))
            {
                return true;
            }

            return false;
        }
    }
}