ALTER TABLE [dbo].ConfigurationParameterValue
	ADD CONSTRAINT [FK_ConfigurationParameterValue_ParameterID_PK_Parameter_ID]
	FOREIGN KEY (ParameterID)
	REFERENCES [Parameter] (ID)
