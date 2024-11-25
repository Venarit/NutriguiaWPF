CREATE TABLE [Catalog].[Objective](
	[idObjective] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](150) NULL,
	[Calories] [int] NOT NULL,
	[Active] [bit] NULL,
	[UpdDateTime] [datetimeoffset](7) NULL,
	[InsDateTime] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[idObjective] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Catalog].[Objective] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Catalog].[Objective] ADD  DEFAULT (getdate()) FOR [UpdDateTime]
GO

ALTER TABLE [Catalog].[Objective] ADD  DEFAULT (getdate()) FOR [InsDateTime]
GO