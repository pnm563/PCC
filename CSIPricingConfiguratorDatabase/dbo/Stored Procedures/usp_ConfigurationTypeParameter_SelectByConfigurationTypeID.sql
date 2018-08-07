CREATE PROCEDURE [dbo].[usp_ConfigurationTypeParameter_SelectByConfigurationTypeID]
	@ConfigurationTypeID NVARCHAR(255)
AS
BEGIN
SELECT ctp.[ID]
      ,ctp.[DateCreated]
      ,ctp.[DateModified]
      ,ctp.[CreatedBy]
      ,ctp.[ModifiedBy]
      ,ctp.[RowVersion]
      ,ctp.[ConfigurationTypeID]
      ,ctp.[ParameterID]
	  ,ctp.[DisplayOrder]
      ,p.Name ParameterName
      ,p.ParameterType1
	  ,p.IsHasValues
	  ,p.Label ParameterLabel
	  
  FROM [ConfigurationTypeParameter] ctp,
  Parameter p WHERE p.ID = ParameterID
  AND CONVERT(NVARCHAR(255), ConfigurationTypeID) = @ConfigurationTypeID

END
