using CSIProductConfigurationCommon.Enums;
using Niu.OneWorkspace.Common.Attributes;
using Niu.OneWorkspace.Common.Entities;
using Niu.OneWorkspace.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class ConfigurationTypeOutput : EntityBase, IEntity
    {
        public String Name { get; set; }
        public String Label { get; set; }
        public String Action { get; set; }
        public ActionType ActionType { get; set; }
        public String ConfigurationTypeID { get; set; }
        public AttributeType ValueType { get; set; }

        [Ignore]
        public bool IsProcessed { get; set; }

        [Ignore]
        public String Value { get; set; }
    }
}
