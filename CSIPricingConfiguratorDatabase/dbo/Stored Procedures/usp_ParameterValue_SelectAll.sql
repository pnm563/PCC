CREATE PROCEDURE [dbo].[usp_ParameterValue_SelectAll]
AS
BEGIN
SELECT [ParameterID]
      ,[Label]
      ,[Value]
  FROM [ParameterValue]

END
