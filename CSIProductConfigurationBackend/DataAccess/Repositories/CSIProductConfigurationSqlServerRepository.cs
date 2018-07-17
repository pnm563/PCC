using CSIProductConfigurationBackend.DataAccess.Interfaces;
using Niu.OneWorkspace.Common.Logging;
using Niu.OneWorkspace.DataAccess.DataProvider.Interfaces;
using Niu.OneWorkspace.DataAccess.Exceptions;
using Niu.OneWorkspace.DataAccess.Mappers.Interfaces;
using Niu.OneWorkspace.DataAccess.Repositories.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.DataAccess.Repositories
{
    public class CSIProductConfigurationSqlServerRepository<T> : GenericSqlServerRepository<T> where T : class
    {
        public ISqlServerDataProvider _dataProvider;
        public ICSIProductConfigurationSqlServerDataMapper<T> _mapper;
        public string _tableName;

        public CSIProductConfigurationSqlServerRepository(ISqlServerDataProvider dataProvider, ICSIProductConfigurationSqlServerDataMapper<T> mapper)
            : base(dataProvider, (ISqlServerDataMapper<T>)mapper)
        {
            _dataProvider = dataProvider;
            _mapper = mapper;
            _tableName = typeof(T).Name;
        }

        public CSIProductConfigurationSqlServerRepository(ISqlServerDataProvider provider, ICSIProductConfigurationSqlServerDataMapper<T> mapper, string tableName)
            : base(provider, (ISqlServerDataMapper<T>)mapper)
        {
            _dataProvider = provider;
            _mapper = mapper;
            _tableName = tableName;
        }

        public override List<T> SelectByProcedureNoConstraints(String tableAction, List<DbParameter> parameters)
        {
            string storedProcedure = string.Format("usp_{0}_{1}", _tableName, tableAction);

            try
            {
                DataTable data = _dataProvider.ExecuteSelectProcedureNoConstraints(storedProcedure, parameters);

                try
                {
                    return _mapper.Map(data);
                }
                catch
                {
                    Logger.Log.WarnFormat("No entites returned by procedure {0}", storedProcedure.ToString());
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Fatal(string.Format("Error executing procedure {0}", storedProcedure.ToString()), ex);
                throw new SelectDataException("An error occured during the select operation", ex);
            }
        }
    }
}