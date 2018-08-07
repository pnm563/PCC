CREATE TABLE [dbo].[ConfigurationTypeParameter] (
    [ID]                  UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]         DATETIME         NOT NULL,
    [DateModified]        DATETIME         NOT NULL,
    [CreatedBy]           NVARCHAR (255)   NOT NULL,
    [ModifiedBy]          NVARCHAR (255)   NOT NULL,
    [RowVersion]          ROWVERSION       NOT NULL,
    [ConfigurationTypeID] UNIQUEIDENTIFIER NOT NULL,
    [ParameterID]         UNIQUEIDENTIFIER NOT NULL, 
    [DisplayOrder] INT NULL
);

