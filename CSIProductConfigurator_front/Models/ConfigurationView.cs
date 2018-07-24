using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSIProductConfigurator_front.Models
{

    public class ConfigurationView
    {
        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
       
        public List<String> CustomerCodes { get; set; }
        public List<ConfigurationType> ConfigurationTypes { get; set; }

        [Display(Name="Customer Code")]
        public String SelectedCustomerCode { get; set; }
        [Display(Name = "Configuration Type")]
        public String SelectedConfigurationType { get; set; }

        public List<ConfigurationParameterValue> ConfigurationParameterValues { get; set; }

        public List<String> ListOfConfigurationParameterValues { get; set; }
    }


}