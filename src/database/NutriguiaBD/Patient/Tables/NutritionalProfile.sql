CREATE TABLE [Patient].[NutritionalProfile](
	[idNutritionalProfile] [int] IDENTITY(1,1) NOT NULL,
	[idPatient] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Sex] [nvarchar](50) NOT NULL,
	[idActivity] [int] NOT NULL,
	[idObjective] [int] NOT NULL,
	[idMacronutrients] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idNutritionalProfile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Patient].[NutritionalProfile] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdDateTime]
GO

ALTER TABLE [Patient].[NutritionalProfile] ADD  DEFAULT (sysdatetimeoffset()) FOR [InsDateTime]
GO

ALTER TABLE [Patient].[NutritionalProfile]  WITH CHECK ADD FOREIGN KEY([idActivity])
REFERENCES [Catalog].[Activity] ([idActivity])
GO

ALTER TABLE [Patient].[NutritionalProfile]  WITH CHECK ADD FOREIGN KEY([idMacronutrients])
REFERENCES [Catalog].[Macronutrients] ([idMacronutrients])
GO

ALTER TABLE [Patient].[NutritionalProfile]  WITH CHECK ADD FOREIGN KEY([idObjective])
REFERENCES [Catalog].[Objective] ([idObjective])
GO