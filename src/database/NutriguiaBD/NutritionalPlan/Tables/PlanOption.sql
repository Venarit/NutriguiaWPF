CREATE TABLE [NutritionalPlan].[PlanOption](
	[idPlanOption] [int] IDENTITY(1,1) NOT NULL,
	[Breakfast] [int] NOT NULL,
	[Collation1] [int] NOT NULL,
	[Meal] [int] NOT NULL,
	[Collation2] [int] NOT NULL,
	[Dinner] [int] NOT NULL,
	[Kcal] [int] NULL,
	[Protein] [decimal](10, 2) NULL,
	[Lipids] [decimal](10, 2) NULL,
	[Hco] [decimal](10, 2) NULL,
	[Active] [bit] NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPlanOption] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [NutritionalPlan].[PlanOption] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [NutritionalPlan].[PlanOption] ADD  DEFAULT (sysdatetime()) FOR [InsDateTime]
GO

ALTER TABLE [NutritionalPlan].[PlanOption] ADD  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [NutritionalPlan].[PlanOption]  WITH CHECK ADD FOREIGN KEY([Breakfast])
REFERENCES [NutritionalPlan].[PlanDish] ([idPlanDish])
GO

ALTER TABLE [NutritionalPlan].[PlanOption]  WITH CHECK ADD FOREIGN KEY([Collation1])
REFERENCES [NutritionalPlan].[PlanDish] ([idPlanDish])
GO

ALTER TABLE [NutritionalPlan].[PlanOption]  WITH CHECK ADD FOREIGN KEY([Collation2])
REFERENCES [NutritionalPlan].[PlanDish] ([idPlanDish])
GO

ALTER TABLE [NutritionalPlan].[PlanOption]  WITH CHECK ADD FOREIGN KEY([Dinner])
REFERENCES [NutritionalPlan].[PlanDish] ([idPlanDish])
GO

ALTER TABLE [NutritionalPlan].[PlanOption]  WITH CHECK ADD FOREIGN KEY([Meal])
REFERENCES [NutritionalPlan].[PlanDish] ([idPlanDish])
GO