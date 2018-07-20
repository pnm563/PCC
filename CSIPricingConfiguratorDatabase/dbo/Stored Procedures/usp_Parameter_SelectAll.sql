CREATE PROCEDURE [dbo].[usp_Parameter_SelectAll]
AS
BEGIN
SELECT [Name]
      ,[IsHasValues]
      ,[ParameterType1]
      ,[ID]
      ,[DateCreated]
      ,[DateModified]
      ,[CreatedBy]
      ,[ModifiedBy]
      ,[RowVersion]
      ,[Label]
  FROM [Parameter]


END
