using CSIProductConfigurationCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class ProcessingStackItem
    {
        public String Name { get; set; }
        public ActionType ActionType { get; set; }
    }
}
