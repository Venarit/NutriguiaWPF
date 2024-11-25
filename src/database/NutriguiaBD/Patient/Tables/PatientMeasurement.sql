CREATE TABLE [Patient].[PatientMeasurement](
	[idPatientMeasurement] [int] IDENTITY(1,1) NOT NULL,
	[idNutritionalProfile] [int] NOT NULL,
	[Weight] [decimal](10, 2) NOT NULL,
	[BodyFat] [decimal](10, 2) NULL,
	[BMR] [int] NULL,
	[TDEE] [int] NULL,
	[Active] [bit] NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPatientMeasurement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Patient].[PatientMeasurement] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Patient].[PatientMeasurement] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdDateTime]
GO

ALTER TABLE [Patient].[PatientMeasurement] ADD  DEFAULT (sysdatetimeoffset()) FOR [InsDateTime]
GO

ALTER TABLE [Patient].[PatientMeasurement]  WITH CHECK ADD FOREIGN KEY([idNutritionalProfile])
REFERENCES [Patient].[NutritionalProfile] ([idNutritionalProfile])
GO