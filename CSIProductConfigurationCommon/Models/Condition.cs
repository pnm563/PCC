using CSIProductConfigurationCommon.Enums;
using Niu.OneWorkspace.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class Condition
    {
        public String Question { get; set; }
        public String Name { get; set; }
        public String TrueActionName { get; set; }
        public ActionType TrueActionType { get; set; }
        public String FalseActionName { get; set; }
        public ActionType FalseActionType { get; set; }

        [Ignore]
        public bool IsProcessed { get; set; }

        [Ignore]
        public bool Value { get; set; }

    }
}
