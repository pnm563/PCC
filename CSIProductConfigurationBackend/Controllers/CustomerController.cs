using CSIProductConfigurationBackend.CSIProductConfiguration;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CSIProductConfigurationBackend.Controllers
{
    public class CustomerController : ApiController
    {
        public List<Customer> Get()
        {
            CustomerContext _ctx = new CustomerContext();
            return _ctx.GetCustomers();
        }
    }
}