CREATE TABLE [Food].[DishFood](
	[idDishFood] [int] IDENTITY(1,1) NOT NULL,
	[idDish] [int] NOT NULL,
	[idFood] [int] NOT NULL,
	[Equivalent] [decimal](10, 1) NOT NULL,
	[Quantity] [decimal](10, 1) NOT NULL,
	[Kcal] [int] NULL,
	[Protein] [decimal](10, 2) NULL,
	[Lipids] [decimal](10, 2) NULL,
	[Hco] [decimal](10, 2) NULL,
	[Active] [bit] NULL,
	[UpdDateTime] [datetimeoffset](7) NULL,
	[InsDateTime] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDishFood] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Food].[DishFood] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Food].[DishFood] ADD  DEFAULT (getdate()) FOR [UpdDateTime]
GO

ALTER TABLE [Food].[DishFood] ADD  DEFAULT (getdate()) FOR [InsDateTime]
GO

ALTER TABLE [Food].[DishFood]  WITH CHECK ADD FOREIGN KEY([idDish])
REFERENCES [Food].[Dish] ([idDish])
GO
