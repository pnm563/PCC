CREATE TABLE [dbo].[ConfigurationParameterValue] (
    [ID]              UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]     DATETIME         NOT NULL,
    [DateModified]    DATETIME         NOT NULL,
    [CreatedBy]       NVARCHAR (255)   NOT NULL,
    [ModifiedBy]      NVARCHAR (255)   NOT NULL,
    [RowVersion]      ROWVERSION       NOT NULL,
    [ParameterID]     UNIQUEIDENTIFIER NOT NULL,
    [Value]           NVARCHAR (255)   NOT NULL,
    [ConfigurationID] UNIQUEIDENTIFIER NOT NULL
);

