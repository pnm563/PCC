using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class ConfigurationDetail : Configuration
    {
        public List<ConfigurationParameterValue> ConfigurationParameterValues { get; set; }
        public List<ConfigurationTypeOutput> ConfigurationTypeOutputs { get; set; }
        public bool IsError { get; set; }
        public String OutputText { get; set; }
    }
}
