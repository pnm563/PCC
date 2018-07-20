CREATE PROCEDURE [dbo].[usp_ConfigurationTypeOutput_SelectByConfigurationTypeID]
	@ConfigurationTypeID NVARCHAR(255)
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
  WHERE CONVERT(NVARCHAR(255), ConfigurationTypeID) = @ConfigurationTypeID

END
