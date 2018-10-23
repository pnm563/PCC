using AutomationCommon.Filters;
using CSIProductConfigurationBackend.CSIProductConfiguration;
using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSIProductConfigurationBackend.Controllers
{
    [OAUTHattribute]
    public class CaseController : ApiController
    {
        public List<Case> Get()
        {
            CaseContext _ctx = new CaseContext();
            return _ctx.GetCases();
        }
    }
}
