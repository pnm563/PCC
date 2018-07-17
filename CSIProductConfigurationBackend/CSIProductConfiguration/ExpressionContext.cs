using CSIProductConfigurationBackend.BusinessLogic;
using CSIProductConfigurationCommon;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.CSIProductConfiguration
{
    public class ExpressionContext : ContextBase
    {
        public List<Expression> GetExpressions()
        {
            return CSIProductConfigurationRepositoryFactory.ExpressionRepository.SelectAll();
        }
    }
}