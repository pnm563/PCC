ALTER TABLE [dbo].[ConfigurationTypeOutput]
	ADD CONSTRAINT [FK_ConfigurationTypeOutput_ConfigurationTypeID_PK_ConfigurationType_ID]
	FOREIGN KEY (ConfigurationTypeID)
	REFERENCES [ConfigurationType] (ID)
	ON DELETE CASCADE