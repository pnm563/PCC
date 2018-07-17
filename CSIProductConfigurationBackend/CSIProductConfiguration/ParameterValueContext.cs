using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
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
    }
}