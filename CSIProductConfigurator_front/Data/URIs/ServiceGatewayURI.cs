﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurator_front.Data.URIs
{
    public static class ServiceGatewayURI
    {
        #region ConfigurationType

        public const String ConfigurationTypeURI = "API/ConfigurationType";

        #endregion

        #region ConfigurationTypeParameter

        public const String ConfigurationTypeParameterURI = "API/ConfigurationTypeParameter";
        public const String GetConfigurationTypeParameterByConfigurationTypeIDURI = ConfigurationTypeParameterURI + "?configurationTypeID={0}";

        #endregion

        #region ConfigurationTypeOutput

        public const String ConfigurationTypeOutputURI = "API/ConfigurationTypeOutput";
        public const String GetConfigurationTypeOutputByConfigurationTypeIDURI = ConfigurationTypeOutputURI + "?configurationTypeID={0}";
        
        #endregion

        #region ParameterValue

        public const String ParameterValueURI = "API/ParameterValue";
        public const String GetParameterValueByParameterIDURI = ParameterValueURI + "?parameterID={0}";

        #endregion

        #region ConfigurationDetail

        public const String ConfigurationDetailURI = "API/ConfigurationDetail";

        #endregion

        #region Parameter

        public const String ParameterURI = "API/Parameter";

        #endregion

        #region Customer

        public const String CustomerURI = "API/Customer";

        #endregion

    }
}