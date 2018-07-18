ALTER TABLE [dbo].ConfigurationParameterValue
	ADD CONSTRAINT [FK_ConfigurationParameterValue_ConfigurationID_PK_Configuration_ID]
	FOREIGN KEY (ConfigurationID)
	REFERENCES [Configuration] (ID)
