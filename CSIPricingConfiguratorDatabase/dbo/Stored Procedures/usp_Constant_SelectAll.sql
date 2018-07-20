CREATE PROCEDURE [dbo].[usp_Constant_SelectAll]
AS
BEGIN
SELECT [Name]
      ,[Value]
      ,[ValueType]
  FROM [Constant]

END
