using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class ConfigurationParameterValueContext : ContextBase
    {
        public List<ConfigurationParameterValue> GetConfigurations()
        {
            return CSIProductConfigurationRepositoryFactory.ConfigurationParameterValueRepository.SelectAll();
        }
    }
}