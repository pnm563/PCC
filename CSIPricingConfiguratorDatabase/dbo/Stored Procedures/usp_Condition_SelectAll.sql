-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Condition_SelectAll]
AS
BEGIN
SELECT [Question]
      ,[TrueActionName]
      ,[TrueActionType]
      ,[FalseActionName]
      ,[FalseActionType]
      ,[Name]
  FROM [CSIPricingConfigurator].[dbo].[Condition]


END
