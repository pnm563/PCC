CREATE PROCEDURE usp_CaseValue_SelectAll
AS
BEGIN
SELECT [CaseName]
      ,[Value]
      ,[ActionName]
      ,[ActionType]
  FROM [CaseValue]
END
