using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationBackend.Data;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class CustomerContext : ContextBase
    {
            public List<Customer> GetCustomers()
            {
                return CSIProductConfigurationRepositoryFactory.CustomerRepository.SelectAll();
            }

    }
}