ALTER TABLE [dbo].[ConfigurationTypeParameter]
	ADD CONSTRAINT [FK_ConfigurationTypeParameter_ParameterID_PK_Parameter_ID]
	FOREIGN KEY (ParameterID)
	REFERENCES [dbo].[Parameter] (ID)
	ON DELETE CASCADE