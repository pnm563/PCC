﻿using CSIProductConfigurationBackend.CSIProductConfiguration;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSIProductConfigurationBackend.Controllers
{
    public class ConfigurationTypeOutputController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public List<ConfigurationTypeOutput> Get(String configurationTypeID)
        {
            ConfigurationTypeOutputContext _otx = new ConfigurationTypeOutputContext();

            List<ConfigurationTypeOutput> debugList = new List<ConfigurationTypeOutput>();

            debugList = _otx.GetConfigurationTypeOutputsByConfigurationTypeID(configurationTypeID);

            return debugList;
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