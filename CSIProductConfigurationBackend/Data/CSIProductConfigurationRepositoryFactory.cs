using CSIProductConfigurationBackend.Data.Interfaces;
using CSIProductConfigurationBackend.DataAccess;
using CSIProductConfigurationCommon;
using CSIProductConfigurationCommon.Models;
using Niu.OneWorkspace.DataAccess.DataProvider;
using Niu.OneWorkspace.DataAccess.DataProvider.Interfaces;
using Niu.OneWorkspace.DataAccess.Repositories.SqlServer;
using Niu.OneWorkspace.DataAccess.Repositories.SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.Data
{
    public class CSIProductConfigurationRepositoryFactory : ICSIProductConfigurationRepositoryFactory
    {
        /// <summary>
        /// Default constructor that uses the connection string name OneWorkspaceSqlConnection
        /// </summary>
        public CSIProductConfigurationRepositoryFactory()
        {
            ISqlServerDataProvider dataProvider = new SqlServerDataProvider("CSIProductConfigurationSqlConnection");
            InitialiseRepositories(dataProvider);
        }

        /// <summary>
        /// Overloaded constructor, you can pass in your own data context
        /// </summary>
        /// <param name="dataContext"></param>
        public CSIProductConfigurationRepositoryFactory(ISqlServerDataProvider dataProvider)
        {
            InitialiseRepositories(dataProvider);
        }

        private void InitialiseRepositories(ISqlServerDataProvider dataProvider)
        {
            ExpressionRepository = new GenericSqlServerRepository<Expression>(dataProvider, new SqlServerDataMapper<Expression>());
            CaseRepository = new GenericSqlServerRepository<Case>(dataProvider, new SqlServerDataMapper<Case>());
            CaseValueRepository = new GenericSqlServerRepository<CaseValue>(dataProvider, new SqlServerDataMapper<CaseValue>());
            ConditionRepository = new GenericSqlServerRepository<Condition>(dataProvider, new SqlServerDataMapper<Condition>());
            ConfigurationRepository = new GenericSqlServerRepository<Configuration>(dataProvider, new SqlServerDataMapper<Configuration>());
            ConfigurationParameterValueRepository = new GenericSqlServerRepository<ConfigurationParameterValue>(dataProvider, new SqlServerDataMapper<ConfigurationParameterValue>());
            ConfigurationTypeRepository = new GenericSqlServerRepository<ConfigurationType>(dataProvider, new SqlServerDataMapper<ConfigurationType>());
            ConfigurationTypeOutputRepository = new GenericSqlServerRepository<ConfigurationTypeOutput>(dataProvider, new SqlServerDataMapper<ConfigurationTypeOutput>());
            ConfigurationTypeParameterRepository = new GenericSqlServerRepository<ConfigurationTypeParameter>(dataProvider, new SqlServerDataMapper<ConfigurationTypeParameter>());
            ConstantRepository = new GenericSqlServerRepository<Constant>(dataProvider, new SqlServerDataMapper<Constant>());
            ParameterRepository = new GenericSqlServerRepository<Parameter>(dataProvider, new SqlServerDataMapper<Parameter>());
            ParameterValueRepository = new GenericSqlServerRepository<ParameterValue>(dataProvider, new SqlServerDataMapper<ParameterValue>());
        }

        public ISqlServerRepository<Expression> ExpressionRepository { get; private set; }
        public ISqlServerRepository<Case> CaseRepository { get; private set; }
        public ISqlServerRepository<CaseValue> CaseValueRepository { get; private set; }
        public ISqlServerRepository<Condition> ConditionRepository { get; private set; }
        public ISqlServerRepository<Configuration> ConfigurationRepository { get; private set; }
        public ISqlServerRepository<ConfigurationParameterValue> ConfigurationParameterValueRepository { get; private set; }
        public ISqlServerRepository<ConfigurationType> ConfigurationTypeRepository { get; private set; }
        public ISqlServerRepository<ConfigurationTypeOutput> ConfigurationTypeOutputRepository { get; private set; }
        public ISqlServerRepository<ConfigurationTypeParameter> ConfigurationTypeParameterRepository { get; private set; }
        public ISqlServerRepository<Constant> ConstantRepository { get; private set; }
        public ISqlServerRepository<Parameter> ParameterRepository { get; private set; }
        public ISqlServerRepository<ParameterValue> ParameterValueRepository { get; private set; }
    }
}