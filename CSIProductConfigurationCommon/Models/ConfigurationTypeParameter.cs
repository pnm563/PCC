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
    public class ConfigurationTypeParameter : EntityBase, IEntity
    {
        public String ConfigurationTypeID { get; set; }
        public String ParameterID { get; set; }
        public String ParameterName { get; set; }
        public String ParameterType { get; set; }

        [Ignore]
        public bool IsProcessed { get; set; }

    }
}
