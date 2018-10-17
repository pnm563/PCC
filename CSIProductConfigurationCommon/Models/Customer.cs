using Niu.OneWorkspace.Common.Entities;
using Niu.OneWorkspace.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSIProductConfigurationCommon.Models
{
    public class Customer : EntityBase, IEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
