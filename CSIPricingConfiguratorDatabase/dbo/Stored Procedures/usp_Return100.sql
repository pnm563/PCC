CREATE PROCEDURE [dbo].[usp_Return100]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2,100
RETURN 0
