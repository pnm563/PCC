using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class CaseValueContext : ContextBase
    {
        public List<CaseValue> GetCaseValues()
        {
            return CSIProductConfigurationRepositoryFactory.CaseValueRepository.SelectAll();
        }
    }
}