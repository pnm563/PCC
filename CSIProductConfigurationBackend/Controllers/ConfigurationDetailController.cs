﻿using AutomationCommon.Filters;
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
    [OAUTHattribute]
    public class ConfigurationDetailController : ApiController
    {
        public ConfigurationDetail Post(ConfigurationDetail ctdView)
        {
            ConfigurationTypeOutputContext _ctoctx = new ConfigurationTypeOutputContext();
            HttpRequestMessage request = Request;
            ctdView.ConfigurationTypeOutputs = _ctoctx.GetConfigurationTypeOutputsByConfigurationTypeID(ctdView.ConfigurationTypeID);
            
            ConfigurationDetailContext _ctx = new ConfigurationDetailContext();
            ctdView = _ctx.GetPricing(ctdView);
            
            return ctdView;
        }

        public ConfigurationDetail Get()
        {
            ConfigurationTypeContext _cttx = new ConfigurationTypeContext();
            ConfigurationType configurationType = _cttx.GetConfigurationTypeByName("iSeries Powercloud");

            ConfigurationDetailContext _ctx = new ConfigurationDetailContext();
            ConfigurationDetail configurationDetail = new ConfigurationDetail() { CustomerID = "31f4afd2-f2fe-45cf-90a4-00b0d2dea815", ConfigurationTypeID = configurationType.Id, Name = "Test1", Id = Guid.NewGuid().ToString() };

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
                configurationDetail.ConfigurationParameterValues.Add(new ConfigurationParameterValue() { ConfigurationID = configurationDetail.Id, ParameterID = parameter.ParameterID, ParameterName = parameter.ParameterName, ParameterType1 = parameter.ParameterType1, Value = value });
            }

            ConfigurationTypeOutputContext _ctoctx = new ConfigurationTypeOutputContext();
            configurationDetail.ConfigurationTypeOutputs = _ctoctx.GetConfigurationTypeOutputsByConfigurationTypeID(configurationDetail.ConfigurationTypeID);

            configurationDetail = _ctx.GetPricing(configurationDetail);

            //return new HttpResponseMessage() { Content = new StringContent(configurationDetail.OutputText, Encoding.UTF8, "text/html") };
            return configurationDetail;
        }
    }
}
