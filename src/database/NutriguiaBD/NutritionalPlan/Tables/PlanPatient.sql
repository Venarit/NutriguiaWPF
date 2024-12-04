CREATE TABLE [NutritionalPlan].[PlanPatient](
	[idPlanPatient] [int] IDENTITY(1,1) NOT NULL,
	[idPlan] [int] NOT NULL,
	[idPatient] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPlanPatient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [NutritionalPlan].[PlanPatient] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [NutritionalPlan].[PlanPatient] ADD  DEFAULT (sysdatetime()) FOR [InsDateTime]
GO

ALTER TABLE [NutritionalPlan].[PlanPatient] ADD  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [NutritionalPlan].[PlanPatient]  WITH CHECK ADD FOREIGN KEY([idPatient])
REFERENCES [Patient].[Patient] ([idPatient])
GO

ALTER TABLE [NutritionalPlan].[PlanPatient]  WITH CHECK ADD FOREIGN KEY([idPlan])
REFERENCES [NutritionalPlan].[Plan] ([idPlan])
GO
