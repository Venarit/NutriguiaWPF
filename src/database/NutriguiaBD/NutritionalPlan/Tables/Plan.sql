CREATE TABLE [NutritionalPlan].[Plan](
	[idPlan] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](150) NULL,
	[idPlanOption_1] [int] NOT NULL,
	[idPlanOption_2] [int] NULL,
	[idPlanOption_3] [int] NULL,
	[Active] [bit] NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPlan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [NutritionalPlan].[Plan] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [NutritionalPlan].[Plan] ADD  DEFAULT (sysdatetime()) FOR [InsDateTime]
GO

ALTER TABLE [NutritionalPlan].[Plan] ADD  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [NutritionalPlan].[Plan]  WITH CHECK ADD FOREIGN KEY([idPlanOption_1])
REFERENCES [NutritionalPlan].[PlanOption] ([idPlanOption])
GO

ALTER TABLE [NutritionalPlan].[Plan]  WITH CHECK ADD FOREIGN KEY([idPlanOption_2])
REFERENCES [NutritionalPlan].[PlanOption] ([idPlanOption])
GO

ALTER TABLE [NutritionalPlan].[Plan]  WITH CHECK ADD FOREIGN KEY([idPlanOption_3])
REFERENCES [NutritionalPlan].[PlanOption] ([idPlanOption])
GO