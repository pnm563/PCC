using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationBackend.Data;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class CaseContext : ContextBase
    {
        public List<Case> GetCases()
        {
            return CSIProductConfigurationRepositoryFactory.CaseRepository.SelectAll();
        }

    }
}