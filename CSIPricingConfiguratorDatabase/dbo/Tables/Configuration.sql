CREATE TABLE [dbo].[Configuration] (
    [ID]                  UNIQUEIDENTIFIER NOT NULL,
    [Name]                NVARCHAR (255)   NOT NULL,
    [Description]         NVARCHAR (4000)  NOT NULL,
    [CustomerCode]        NVARCHAR (255)   NOT NULL,
    [DateCreated]         DATETIME         NOT NULL,
    [DateModified]        DATETIME         NOT NULL,
    [CreatedBy]           NVARCHAR (255)   NOT NULL,
    [ModifiedBy]          NVARCHAR (255)   NOT NULL,
    [RowVersion]          ROWVERSION       NOT NULL,
    [ConfigurationTypeID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_Configuration] PRIMARY KEY ([ID])
);

