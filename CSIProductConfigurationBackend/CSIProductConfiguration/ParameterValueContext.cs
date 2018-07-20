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
    public class ParameterValueContext : ContextBase
    {
        public List<ParameterValue> GetParameterValues()
        {
            return CSIProductConfigurationRepositoryFactory.ParameterValueRepository.SelectAll();
        }

        public List<ParameterValue> GetParameterValuesByParameterID (String parameterID)
        {
            string tableAction = "SelectByParameterID";

            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter paramId = new SqlParameter("@parameterID", System.Data.SqlDbType.NVarChar);

            paramId.Value = parameterID;
            parameters.Add(paramId);

            List<ParameterValue> debugList = new List<ParameterValue>();

            debugList = CSIProductConfigurationRepositoryFactory.ParameterValueRepository.SelectByProcedure(tableAction, parameters);

            return debugList;
        }
    }
}