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
        [Required]
        public String Description { get; set; }
        [Required,Display(Name = "Customer Name")]
        public String CustomerName { get; set; }

        //public List<String> CustomerCodes { get; set; } using typeAhead search instead
        public List<ConfigurationType> ConfigurationTypes { get; set; }

        [Display(Name = "Customer Code"),Required(ErrorMessage = "The Customer Code is required. Please select a customer to obtain code.")]
        public String SelectedCustomerCode { get; set; }
        [Display(Name = "Configuration Type")]
        public String SelectedConfigurationType { get; set; }

        public int JustToGetViewInt  {get; set;}
        public float JustToGetViewFloat {get; set;}
        public DateTime JustToGetViewDateTime { get; set; }
        public bool JustToGetViewBool { get; set; }

        public List<ConfigurationParameterValue> ConfigurationParameterValues { get; set; }

        public List<String> ListOfConfigurationParameterValues { get; set; }
    }


}