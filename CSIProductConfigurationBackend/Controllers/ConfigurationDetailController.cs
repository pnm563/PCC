using CSIProductConfigurationBackend.CSIProductConfiguration;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace CSIProductConfigurationBackend.Controllers
{
    public class ConfigurationDetailController : ApiController
    {
        public HttpResponseMessage Get()
        {
            ConfigurationTypeContext _cttx = new ConfigurationTypeContext();
            ConfigurationType configurationType = _cttx.GetConfigurationTypeByName("iSeries Powercloud");

            ConfigurationDetailContext _ctx = new ConfigurationDetailContext();
            ConfigurationDetail configurationDetail = new ConfigurationDetail() { CustomerCode = "9999", ConfigurationTypeID = configurationType.Id, Name = "Test1", Id = Guid.NewGuid().ToString() };

            ConfigurationTypeParameterContext _ptx = new ConfigurationTypeParameterContext();

            List<ConfigurationTypeParameter> parameters = _ptx.GetConfigurationTypeParametersByConfigurationTypeID(configurationType.Id);

            configurationDetail.ConfigurationParameterValues = new List<ConfigurationParameterValue>();

            foreach (ConfigurationTypeParameter parameter in parameters)
            {

                String value = "";
                if (parameter.ParameterName.Equals("CustomerName")) value = "TEST CUSTOMER";
                else if (parameter.ParameterName.Equals("DataCentre")) value = "Equinix";
                else if (parameter.ParameterName.Equals("PowerCloudOffering")) value = "HA";
                else if (parameter.ParameterName.Equals("IBMiVersion")) value = "V7R1";
                else if (parameter.ParameterName.Equals("CPW")) value = "50000";
                else if (parameter.ParameterName.Equals("ContractTerm")) value = "3";
                else if (parameter.ParameterName.Equals("Tier1Storage")) value = "10";
                else if (parameter.ParameterName.Equals("Tier2Storage")) value = "0";
                else if (parameter.ParameterName.Equals("PTFApplications")) value = "Quarterly";
                else if (parameter.ParameterName.Equals("IsBackupManagement")) value = "yes";
                else if (parameter.ParameterName.Equals("IsBackupEncryption")) value = "yes";
                configurationDetail.ConfigurationParameterValues.Add(new ConfigurationParameterValue() { ConfigurationID = configurationDetail.Id, ParameterID = parameter.ParameterID, ParameterName = parameter.ParameterName, ParameterType = parameter.ParameterType, Value = value });
            }

            ConfigurationTypeOutputContext _ctoctx = new ConfigurationTypeOutputContext();
            configurationDetail.ConfigurationTypeOutputs = _ctoctx.GetConfigurationTypeOutputsByConfigurationTypeID(configurationType.Id);

            configurationDetail = _ctx.GetPricing(configurationDetail);
            return new HttpResponseMessage() { Content = new StringContent(configurationDetail.OutputText, Encoding.UTF8, "text/html") };
        }
    }
}
