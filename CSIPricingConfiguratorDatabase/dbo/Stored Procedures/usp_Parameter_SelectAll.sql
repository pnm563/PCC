CREATE PROCEDURE [dbo].[usp_Parameter_SelectAll]
AS
BEGIN
SELECT [Name]
      ,[IsHasValues]
      ,[ParameterType]
      ,[ID]
      ,[DateCreated]
      ,[DateModified]
      ,[CreatedBy]
      ,[ModifiedBy]
      ,[RowVersion]
      ,[Label]
  FROM [CSIPricingConfigurator].[dbo].[Parameter]


END
