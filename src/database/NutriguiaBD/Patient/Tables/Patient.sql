CREATE TABLE [Patient].[Patient](
	[idPatient] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SecondName] [nvarchar](50) NULL,
	[LastNameP] [nvarchar](50) NOT NULL,
	[LastNameM] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
	[UpdDateTime] [datetimeoffset](7) NOT NULL,
	[InsDateTime] [datetimeoffset](7) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Cellphone] [nvarchar](10) NULL,
	[BirthDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPatient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Patient].[Patient] ADD  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [Patient].[Patient] ADD  DEFAULT (sysdatetimeoffset()) FOR [UpdDateTime]
GO

ALTER TABLE [Patient].[Patient] ADD  DEFAULT (sysdatetimeoffset()) FOR [InsDateTime]
GO