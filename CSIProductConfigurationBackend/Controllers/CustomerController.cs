using CSIProductConfigurationBackend.CSIProductConfiguration;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.Controllers
{
    public class CustomerController
    {
        public List<Customer> Get()
        {
            CustomerContext _ctx = new CustomerContext();
            return _ctx.GetCustomers();
        }
    }
}