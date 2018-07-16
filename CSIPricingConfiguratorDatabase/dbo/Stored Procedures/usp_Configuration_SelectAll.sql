CREATE PROCEDURE usp_Configuration_SelectAll
AS
BEGIN
SELECT [ID]
      ,[Name]
      ,[Description]
      ,[CustomerCode]
      ,[DateCreated]
      ,[DateModified]
      ,[CreatedBy]
      ,[ModifiedBy]
      ,[RowVersion]
      ,[ConfigurationTypeID]
  FROM [CSIPricingConfigurator].[dbo].[Configuration]

END
