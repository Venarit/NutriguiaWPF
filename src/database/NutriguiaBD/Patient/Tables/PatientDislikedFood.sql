CREATE TABLE [Patient].[PatientDislikedFood](
	[idPatient] [int] NOT NULL,
	[idFood] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL
);
GO

ALTER TABLE [Patient].[PatientDislikedFood] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Patient].[PatientDislikedFood] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdDateTime]
GO

ALTER TABLE [Patient].[PatientDislikedFood] ADD  DEFAULT (sysdatetimeoffset()) FOR [InsDateTime]
GO

ALTER TABLE [Patient].[PatientDislikedFood]  WITH CHECK ADD FOREIGN KEY([idPatient])
REFERENCES [Patient].[Patient] ([idPatient])
GO

ALTER TABLE [Patient].[PatientDislikedFood]  WITH CHECK ADD FOREIGN KEY([idFood])
REFERENCES [Food].[Food] ([idFood])
GO