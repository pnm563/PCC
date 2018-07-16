CREATE PROCEDURE [dbo].[usp_ConfigurationTypeParameter_SelectAll]
AS
BEGIN
SELECT ctp.[ID]
      ,ctp.[DateCreated]
      ,ctp.[DateModified]
      ,ctp.[CreatedBy]
      ,ctp.[ModifiedBy]
      ,ctp.[RowVersion]
      ,ctp.[ConfigurationTypeID]
      ,ctp.[ParameterID]
      ,p.Name ParameterName
      ,p.ParameterType
  FROM [CSIPricingConfigurator].[dbo].[ConfigurationTypeParameter] ctp,
  Parameter p WHERE p.ID = ParameterID


END
