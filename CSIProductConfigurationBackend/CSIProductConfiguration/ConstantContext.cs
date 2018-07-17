using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class ConstantContext : ContextBase
    {
        public List<Constant> GetConstants()
        {
            return CSIProductConfigurationRepositoryFactory.ConstantRepository.SelectAll();
        }
    }
}