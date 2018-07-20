CREATE PROCEDURE [dbo].[usp_ConfigurationType_SelectAll]
AS
BEGIN
SELECT [ID]
      ,[DateCreated]
      ,[DateModified]
      ,[CreatedBy]
      ,[ModifiedBy]
      ,[RowVersion]
      ,[Name]
  FROM [ConfigurationType]

END
