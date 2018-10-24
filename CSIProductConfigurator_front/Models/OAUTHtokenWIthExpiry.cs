using AutomationCommon.Model.OAUTH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurator_front.Models
{
    public class OAUTHtokenWIthExpiry : OAUTHtoken
    {
        DateTime expiry { get; set; }
    }
}