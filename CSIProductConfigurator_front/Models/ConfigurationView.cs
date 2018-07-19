using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurator_front.Models
{

    public class ConfigurationView
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public List<String> CustomerCodes { get; set; }
        public List<ConfigurationType> ConfigurationTypes { get; set; }
        public List<ConfigurationParameterValue> ConfigurationParameterValues { get; set; }

    }


}