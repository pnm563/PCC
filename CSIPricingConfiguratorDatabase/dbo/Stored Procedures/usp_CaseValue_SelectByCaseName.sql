CREATE PROCEDURE usp_CaseValue_SelectByCaseName
	@CaseName NVARCHAR(255)
AS
BEGIN
SELECT [CaseName]
      ,[Value]
      ,[ActionName]
      ,[ActionType]
  FROM [CSIPricingConfigurator].[dbo].[CaseValue]
  WHERE CaseName = @CaseName
END
