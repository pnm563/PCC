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
    public class ConfigurationTypeParameterController : ApiController
    {
        public List<ConfigurationTypeParameter> Get()
        {
            ConfigurationTypeParameterContext _ptx = new ConfigurationTypeParameterContext();

            return _ptx.GetConfigurationTypeParameters();
        }

        // GET api/<controller>
        public List<ConfigurationTypeParameter> Get(String configurationTypeID)
        {
            ConfigurationTypeParameterContext _ptx = new ConfigurationTypeParameterContext();

            return _ptx.GetConfigurationTypeParametersByConfigurationTypeID(configurationTypeID);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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