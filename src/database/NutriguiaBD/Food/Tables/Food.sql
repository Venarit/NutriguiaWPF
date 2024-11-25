CREATE TABLE [Food].[Food](
	[idFood] [int] IDENTITY(1,1) NOT NULL,
	[idFoodType] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Quantity] [numeric](18, 2) NOT NULL,
	[idUnit] [int] NOT NULL,
	[GrossWeight] [numeric](18, 2) NOT NULL,
	[NetWeight] [numeric](18, 2) NOT NULL,
	[Energy] [numeric](18, 2) NOT NULL,
	[Protein] [numeric](18, 2) NOT NULL,
	[Lipids] [numeric](18, 2) NOT NULL,
	[Hco] [numeric](18, 2) NOT NULL,
	[Fiber] [numeric](18, 2) NULL,
	[VitaminA] [numeric](18, 2) NULL,
	[AscorbicAcid] [numeric](18, 2) NULL,
	[FolicAcid] [numeric](18, 2) NULL,
	[Iron] [numeric](18, 2) NULL,
	[Potasium] [numeric](18, 2) NULL,
	[GlycemicIndex] [numeric](18, 2) NULL,
	[GlycemicLoad] [numeric](18, 2) NULL,
	[Sugar] [numeric](18, 2) NULL,
	[Sodium] [numeric](18, 2) NULL,
	[Calcium] [numeric](18, 2) NULL,
	[Selenium] [numeric](18, 2) NULL,
	[Phosphorus] [numeric](18, 2) NULL,
	[Colesterol] [numeric](18, 2) NULL,
	[Active] [bit] NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idFood] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Food].[Food] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Food].[Food] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdDateTime]
GO

ALTER TABLE [Food].[Food] ADD  DEFAULT (sysdatetimeoffset()) FOR [InsDateTime]
GO

ALTER TABLE [Food].[Food]  WITH CHECK ADD FOREIGN KEY([idFoodType])
REFERENCES [Food].[FoodType] ([idFoodType])
GO

ALTER TABLE [Food].[Food]  WITH CHECK ADD FOREIGN KEY([idUnit])
REFERENCES [Food].[Unit] ([idUnit])