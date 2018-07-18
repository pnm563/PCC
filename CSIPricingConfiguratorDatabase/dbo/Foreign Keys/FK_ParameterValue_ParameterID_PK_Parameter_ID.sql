ALTER TABLE [dbo].ParameterValue
	ADD CONSTRAINT [FK_ParameterValue_ParameterID_PK_Parameter_ID]
	FOREIGN KEY (ParameterID)
	REFERENCES [Parameter] (ID)
	ON DELETE CASCADE