CREATE TABLE [dbo].[ConfigurationType] (
    [ID]           UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]  DATETIME         NOT NULL,
    [DateModified] DATETIME         NOT NULL,
    [CreatedBy]    NVARCHAR (255)   NOT NULL,
    [ModifiedBy]   NVARCHAR (255)   NOT NULL,
    [RowVersion]   ROWVERSION       NOT NULL,
    [Name]         NVARCHAR (255)   NOT NULL, 
    CONSTRAINT [PK_ConfigurationType] PRIMARY KEY ([ID])
);

