using Niu.OneWorkspace.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class Case
    {
        public String Name { get; set; }
        public String ExpressionName { get; set; }
        [Ignore]
        public bool IsProcessed { get; set; }
        [Ignore]
        public String Value { get; set; }

    }
}
