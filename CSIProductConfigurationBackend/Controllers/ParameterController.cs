using CSIProductConfigurationBackend.CSIProductConfiguration;
using CSIProductConfigurationCommon.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CSIProductConfigurationBackend.Controllers
{
    public class ParameterController : ApiController
    {
        // GET api/<controller>
        public List<Parameter> Get()
        {
            ParameterContext _pctx = new ParameterContext();

            List<Parameter> debugList = new List<Parameter>();

            debugList = _pctx.GetParameters();

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