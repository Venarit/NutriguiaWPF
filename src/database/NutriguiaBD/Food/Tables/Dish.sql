CREATE TABLE [Food].[Dish](
	[idDish] [int] IDENTITY(1,1) NOT NULL,
	[idDishType] [int] NOT NULL,
	[Code] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NULL,
	[Kcal] [int] NULL,
	[Protein] [decimal](10, 2) NULL,
	[Lipids] [decimal](10, 2) NULL,
	[Hco] [decimal](10, 2) NULL,
	[Active] [bit] NULL,
	[UpdDateTime] [datetimeoffset](7) NULL,
	[InsDateTime] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDish] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Food].[Dish] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Food].[Dish] ADD  DEFAULT (getdate()) FOR [UpdDateTime]
GO

ALTER TABLE [Food].[Dish] ADD  DEFAULT (getdate()) FOR [InsDateTime]
GO

ALTER TABLE [Food].[Dish]  WITH CHECK ADD FOREIGN KEY([idDishType])
REFERENCES [Food].[DishType] ([idDishType])
GO
