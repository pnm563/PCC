CREATE PROCEDURE [dbo].[usp_Constant_SelectAll]
AS
BEGIN
SELECT [Name]
      ,[Value]
      ,[ValueType]
  FROM [CSIPricingConfigurator].[dbo].[Constant]

END
