CREATE PROCEDURE [dbo].[usp_ConfigurationType_SelectByName]
	@Name NVARCHAR(255)
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
  WHERE Name = @Name

END
