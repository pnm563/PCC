CREATE PROCEDURE [dbo].[usp_Expression_SelectAll]
AS
BEGIN
SELECT [Name]
      ,[Code]
      ,[ValueType]
  FROM [Expression]
END
