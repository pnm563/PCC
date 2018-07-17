using CSIProductConfigurationBackend.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.BusinessLogic.Interface
{
    public interface IContext
    {
        ICSIProductConfigurationRepositoryFactory CSIProductConfigurationRepositoryFactory { get; }
    }
}