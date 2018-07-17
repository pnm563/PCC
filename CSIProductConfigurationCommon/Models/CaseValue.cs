using CSIProductConfigurationCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class CaseValue
    {
        public String CaseName { get; set; }
        public String Value { get; set; }
        public String ActionName { get; set; }
        public ActionType ActionType { get; set; }
    }
}
