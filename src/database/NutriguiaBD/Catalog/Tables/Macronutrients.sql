CREATE TABLE [Catalog].[Macronutrients](
	[idMacronutrients] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[Hco] [decimal](2, 2) NOT NULL,
	[Lipids] [decimal](2, 2) NOT NULL,
	[Protein] [decimal](2, 2) NOT NULL,
	[Active] [bit] NULL,
	[UpdDateTime] [datetimeoffset](7) NULL,
	[InsDateTime] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMacronutrients] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Catalog].[Macronutrients] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Catalog].[Macronutrients] ADD  DEFAULT (getdate()) FOR [UpdDateTime]
GO

ALTER TABLE [Catalog].[Macronutrients] ADD  DEFAULT (getdate()) FOR [InsDateTime]
GO
