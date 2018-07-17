using Niu.OneWorkspace.Common.Entities;
using Niu.OneWorkspace.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class ConfigurationParameterValue : EntityBase, IEntity
    {
        public String ParameterID { get; set; }
        public String Value { get; set; }
        public String ConfigurationID { get; set; }
        public String ParameterName { get; set; }
        public String ParameterType { get; set; }
    }
}
