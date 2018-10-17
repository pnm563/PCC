using CSIProductConfigurationCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurator_front.Models
{
    public class TypeAheadResults<T> where T : class
    {
        public TypeAheadResults()
        {
            status = true;
            error = null;
            data = new Results<T>();
        }

        public bool status { get; set; }
        public string error { get; set; }
        public Results<T> data { get; set; }
    }
}