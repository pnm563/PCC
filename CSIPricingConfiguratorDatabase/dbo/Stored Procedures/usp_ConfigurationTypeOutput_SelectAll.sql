CREATE PROCEDURE [dbo].[usp_ConfigurationTypeOutput_SelectAll]
AS
BEGIN
SELECT [ID]
      ,[DateCreated]
      ,[DateModified]
      ,[CreatedBy]
      ,[ModifiedBy]
      ,[RowVersion]
      ,[Name]
      ,[Action]
      ,[ActionType]
      ,[Label]
      ,[ConfigurationTypeID]
      ,[ValueType]
  FROM [ConfigurationTypeOutput]

END
