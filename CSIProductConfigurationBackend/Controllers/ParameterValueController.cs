using AutomationCommon.Filters;
using CSIProductConfigurationBackend.CSIProductConfiguration;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSIProductConfigurationBackend.Controllers
{
    //[OAUTHattribute]
    public class ParameterValueController : ApiController
    {
        // GET api/<controller>
        public List<ParameterValue> Get()
        {

            ParameterValueContext _pvtx = new ParameterValueContext();

            List<ParameterValue> debugList = new List<ParameterValue>();

            debugList = _pvtx.GetParameterValues();

            return debugList;
        }

        // GET api/<controller>
        public List<ParameterValue> Get(String parameterID)
        {
            ParameterValueContext _pvtx = new ParameterValueContext();

            List<ParameterValue> debugList = new List<ParameterValue>();

            debugList = _pvtx.GetParameterValuesByParameterID(parameterID);

            return debugList;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}