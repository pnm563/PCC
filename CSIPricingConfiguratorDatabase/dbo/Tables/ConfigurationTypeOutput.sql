CREATE TABLE [dbo].[ConfigurationTypeOutput] (
    [ID]                  UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]         DATETIME         NOT NULL,
    [DateModified]        DATETIME         NOT NULL,
    [CreatedBy]           NVARCHAR (255)   NOT NULL,
    [ModifiedBy]          NVARCHAR (255)   NOT NULL,
    [RowVersion]          ROWVERSION       NOT NULL,
    [Name]                NVARCHAR (255)   NOT NULL,
    [Action]              NVARCHAR (255)   NOT NULL,
    [ActionType]          INT              NOT NULL,
    [Label]               NVARCHAR (255)   NOT NULL,
    [ConfigurationTypeID] UNIQUEIDENTIFIER NOT NULL,
    [ValueType]           INT              NOT NULL
);

