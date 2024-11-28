CREATE TABLE [dbo].[Appointment](
	[idAppointment] [int] IDENTITY(1,1) NOT NULL,
	[idPatient] [int] NOT NULL,
	[idAppointmentStatus] [int] NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[Notes] [text] NULL,
	[Active] [bit] NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idAppointment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Appointment] ADD  DEFAULT ((1)) FOR [idAppointmentStatus]
GO

ALTER TABLE [dbo].[Appointment] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[Appointment] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdDateTime]
GO

ALTER TABLE [dbo].[Appointment] ADD  DEFAULT (sysdatetimeoffset()) FOR [InsDateTime]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([idAppointmentStatus])
REFERENCES [Catalog].[AppointmentStatus] ([idAppointmentStatus])
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([idPatient])
REFERENCES [Patient].[Patient] ([idPatient])
GO