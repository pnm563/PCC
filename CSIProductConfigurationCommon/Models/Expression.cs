using CSIProductConfigurationCommon.Enums;
using Niu.OneWorkspace.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class Expression
    {
        public String Name { get; set; }
        public String Code { get; set; }
        public AttributeType ValueType { get; set; }

        [Ignore]
        public bool IsProcessed { get; set; }
        [Ignore]
        public String Value { get; set; }
    }
}
