using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class ConditionContext : ContextBase
    {
        public List<Condition> GetConditions()
        {
            return CSIProductConfigurationRepositoryFactory.ConditionRepository.SelectAll();
        }
    }
}