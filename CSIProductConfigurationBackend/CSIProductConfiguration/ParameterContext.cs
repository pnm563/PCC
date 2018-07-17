using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class ParameterContext : ContextBase
    {
        public List<Parameter> GetParameters()
        {
            return CSIProductConfigurationRepositoryFactory.ParameterRepository.SelectAll();
        }
    }
}