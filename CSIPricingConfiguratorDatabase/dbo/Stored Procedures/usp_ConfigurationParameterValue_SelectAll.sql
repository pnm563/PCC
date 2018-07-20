CREATE PROCEDURE usp_ConfigurationParameterValue_SelectAll
AS
BEGIN
SELECT cp.[ID]
      ,cp.[DateCreated]
      ,cp.[DateModified]
      ,cp.[CreatedBy]
      ,cp.[ModifiedBy]
      ,cp.[RowVersion]
      ,cp.[ParameterID]
      ,cp.[Value]
      ,p.Name ParameterName
      ,p.[ParameterType1]
  FROM [ConfigurationParameterValue] cp,
	Parameter p
  WHERE p.ID = cp.ParameterID
END
