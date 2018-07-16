CREATE PROCEDURE [dbo].[usp_ParameterValue_SelectAll]
AS
BEGIN
SELECT [ParameterID]
      ,[Label]
      ,[Value]
  FROM [CSIPricingConfigurator].[dbo].[ParameterValue]

END
