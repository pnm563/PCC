ALTER TABLE [dbo].[Configuration]
	ADD CONSTRAINT [FK_Configuration_ConfigurationTypeID_PK_ConfigurationType_ID]
	FOREIGN KEY (ConfigurationTypeID)
	REFERENCES [dbo].[ConfigurationType] (ID)
