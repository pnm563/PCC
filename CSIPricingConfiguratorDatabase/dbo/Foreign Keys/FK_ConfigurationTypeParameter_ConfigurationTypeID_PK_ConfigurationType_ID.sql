ALTER TABLE [dbo].[ConfigurationTypeParameter]
	ADD CONSTRAINT [FK_ConfigurationTypeParameter_ConfigurationTypeID_PK_ConfigurationType_ID]
	FOREIGN KEY (ConfigurationTypeID)
	REFERENCES [dbo].[ConfigurationType] (ID)
