CREATE PROCEDURE [dbo].[usp_ParameterValue_SelectByParameterID]
	@ParameterID nvarchar(255)
AS
BEGIN
SELECT [ParameterID]
      ,[Label]
      ,[Value]
  FROM [ParameterValue]
  WHERE
  CONVERT(NVARCHAR(255), ParameterID) = @ParameterID
  END
