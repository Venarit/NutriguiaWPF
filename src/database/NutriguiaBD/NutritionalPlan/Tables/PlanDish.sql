CREATE TABLE [NutritionalPlan].[PlanDish](
	[idPlanDish] [int] IDENTITY(1,1) NOT NULL,
	[idDish] [int] NULL,
	[idDishSecondary] [int] NULL,
	[Active] [bit] NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPlanDish] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [NutritionalPlan].[PlanDish] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [NutritionalPlan].[PlanDish] ADD  DEFAULT (sysdatetime()) FOR [InsDateTime]
GO

ALTER TABLE [NutritionalPlan].[PlanDish] ADD  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [NutritionalPlan].[PlanDish]  WITH CHECK ADD FOREIGN KEY([idDish])
REFERENCES [Food].[Dish] ([idDish])
GO

ALTER TABLE [NutritionalPlan].[PlanDish]  WITH CHECK ADD FOREIGN KEY([idDishSecondary])
REFERENCES [Food].[Dish] ([idDish])
GO