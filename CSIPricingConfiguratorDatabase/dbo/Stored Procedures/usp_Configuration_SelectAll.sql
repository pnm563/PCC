CREATE PROCEDURE usp_Configuration_SelectAll
AS
BEGIN
SELECT [ID]
      ,[Name]
      ,[Description]
      ,[CustomerID]
      ,[DateCreated]
      ,[DateModified]
      ,[CreatedBy]
      ,[ModifiedBy]
      ,[RowVersion]
      ,[ConfigurationTypeID]
  FROM [Configuration]

END
