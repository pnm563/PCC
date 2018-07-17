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
    public class ConfigurationTypeContext : ContextBase
    {
        public List<ConfigurationType> GetConfigurationTypes()
        {
            return CSIProductConfigurationRepositoryFactory.ConfigurationTypeRepository.SelectAll();
        }

        public ConfigurationType GetConfigurationTypeByName(String name)
        {
            string tableAction = "SelectByName";

            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter paramId = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);

            paramId.Value = name;
            parameters.Add(paramId);

            List<ConfigurationType> configurationTypes = CSIProductConfigurationRepositoryFactory.ConfigurationTypeRepository.SelectByProcedure(tableAction, parameters);

            if (configurationTypes != null && configurationTypes.Count == 1)
                return configurationTypes.First();
            else
                return null;

        }
    }
}