CREATE TABLE [dbo].[ParameterValue] (
    [ParameterID] UNIQUEIDENTIFIER NOT NULL,
    [Label]       NVARCHAR (255)   NOT NULL,
    [Value]       NVARCHAR (255)   NOT NULL, 
    [ID] UNIQUEIDENTIFIER NOT NULL DEFAULT newid(), 
    CONSTRAINT [PK_ParameterValue] PRIMARY KEY ([ID])
);

