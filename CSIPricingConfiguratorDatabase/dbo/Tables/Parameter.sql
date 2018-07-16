CREATE TABLE [dbo].[Parameter] (
    [Name]          NVARCHAR (255)   NOT NULL,
    [IsHasValues]   BIT              NOT NULL,
    [ParameterType] INT              NOT NULL,
    [ID]            UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]   DATETIME         NOT NULL,
    [DateModified]  DATETIME         NOT NULL,
    [CreatedBy]     NVARCHAR (255)   NOT NULL,
    [ModifiedBy]    NVARCHAR (255)   NOT NULL,
    [RowVersion]    ROWVERSION       NOT NULL,
    [Label]         NVARCHAR (255)   NOT NULL
);

