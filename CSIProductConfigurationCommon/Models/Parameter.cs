using CSIProductConfigurationCommon.Enums;
using Niu.OneWorkspace.Common.Entities;
using Niu.OneWorkspace.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class Parameter : EntityBase, IEntity
    {
        public String Name { get; set; }
        public String Label { get; set; }
        public bool IsHasValues { get; set; }
        public AttributeType ParameterType { get; set; }
    }
}
