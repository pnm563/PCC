CREATE PROCEDURE [dbo].[usp_Expression_SelectAll]
AS
BEGIN
SELECT [Name]
      ,[Code]
      ,[ValueType]
  FROM [CSIPricingConfigurator].[dbo].[Expression]
END
