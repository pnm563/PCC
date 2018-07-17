using Niu.OneWorkspace.Common.Entities;
using Niu.OneWorkspace.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class Configuration : EntityBase, IEntity
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String CustomerCode { get; set; }
        public String ConfigurationTypeID { get; set; }
    }
}
