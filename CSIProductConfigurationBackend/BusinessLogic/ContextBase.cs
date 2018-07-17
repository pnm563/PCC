using CSIProductConfigurationBackend.BusinessLogic.Interface;
using CSIProductConfigurationBackend.Data;
using CSIProductConfigurationBackend.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.BusinessLogic
{
    public abstract class ContextBase : IContext
    {
        private ICSIProductConfigurationRepositoryFactory _CSIProductConfigurationFactory;

        public ICSIProductConfigurationRepositoryFactory CSIProductConfigurationRepositoryFactory
        {
            get
            {
                if (_CSIProductConfigurationFactory == null)
                {
                    _CSIProductConfigurationFactory = new CSIProductConfigurationRepositoryFactory();
                }

                return _CSIProductConfigurationFactory;
            }
        }
    }
}