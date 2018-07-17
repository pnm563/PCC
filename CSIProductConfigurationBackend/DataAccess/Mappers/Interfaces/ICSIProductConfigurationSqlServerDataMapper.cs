using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.DataAccess.Interfaces
{
    public interface ICSIProductConfigurationSqlServerDataMapper<T> where T : class
    {
        T Map(DataRow dataRow);
        List<T> Map(DataTable dataTable);
    }
}