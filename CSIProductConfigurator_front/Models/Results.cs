using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurator_front.Models
{
    public class Results<T> where T : class
    {
        public List<T> TheResults { get; set; }
    }
}