using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class ConfigurationContext : ContextBase
    {
        public List<Configuration> GetConfigurations()
        {
            return CSIProductConfigurationRepositoryFactory.ConfigurationRepository.SelectAll();
        }
    }
}