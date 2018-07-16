CREATE TABLE [dbo].[Condition] (
    [Question]        NVARCHAR (255) NOT NULL,
    [TrueActionName]  NVARCHAR (255) NOT NULL,
    [TrueActionType]  INT            NOT NULL,
    [FalseActionName] NVARCHAR (255) NOT NULL,
    [FalseActionType] INT            NOT NULL,
    [Name]            NVARCHAR (255) NOT NULL
);

