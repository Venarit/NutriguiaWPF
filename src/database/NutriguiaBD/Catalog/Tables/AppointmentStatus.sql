CREATE TABLE [Catalog].[AppointmentStatus](
	[idAppointmentStatus] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[Active] [bit] NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idAppointmentStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Catalog].[AppointmentStatus] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Catalog].[AppointmentStatus] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdDateTime]
GO

ALTER TABLE [Catalog].[AppointmentStatus] ADD  DEFAULT (sysdatetimeoffset()) FOR [InsDateTime]
GO


