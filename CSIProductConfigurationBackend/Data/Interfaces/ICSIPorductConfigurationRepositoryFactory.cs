using CSIProductConfigurationCommon;
using CSIProductConfigurationCommon.Models;
using Niu.OneWorkspace.DataAccess.Repositories.SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIProductConfigurationBackend.Data.Interfaces
{
    public interface ICSIProductConfigurationRepositoryFactory
    {
        ISqlServerRepository<Expression> ExpressionRepository { get; }
        ISqlServerRepository<Case> CaseRepository { get; }
        ISqlServerRepository<CaseValue> CaseValueRepository { get; }
        ISqlServerRepository<Condition> ConditionRepository { get; }
        ISqlServerRepository<Configuration> ConfigurationRepository { get; }
        ISqlServerRepository<ConfigurationParameterValue> ConfigurationParameterValueRepository { get; }
        ISqlServerRepository<ConfigurationType> ConfigurationTypeRepository { get; }
        ISqlServerRepository<ConfigurationTypeOutput> ConfigurationTypeOutputRepository { get; }
        ISqlServerRepository<ConfigurationTypeParameter> ConfigurationTypeParameterRepository { get; }
        ISqlServerRepository<Constant> ConstantRepository { get; }
        ISqlServerRepository<Parameter> ParameterRepository { get; }
        ISqlServerRepository<ParameterValue> ParameterValueRepository { get; }
    }
}