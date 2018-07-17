using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class ConfigurationTypeOutputContext : ContextBase
    {
        public List<ConfigurationTypeOutput> GetConfigurationTypeOutputs()
        {
            return CSIProductConfigurationRepositoryFactory.ConfigurationTypeOutputRepository.SelectAll();
        }

        public List<ConfigurationTypeOutput> GetConfigurationTypeOutputsByConfigurationTypeID(String configurationTypeID)
        {
            string tableAction = "SelectByConfigurationTypeID";

            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter paramId = new SqlParameter("@configurationTypeID", System.Data.SqlDbType.NVarChar);

            paramId.Value = configurationTypeID;
            parameters.Add(paramId);

            return CSIProductConfigurationRepositoryFactory.ConfigurationTypeOutputRepository.SelectByProcedure(tableAction, parameters);


        }
    }
}