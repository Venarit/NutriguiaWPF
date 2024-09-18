﻿CREATE TABLE [Alimento].[Unidad]
(
	[idUnidad] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Codigo] NVARCHAR(100) NOT NULL,
	[Nombre] NVARCHAR(100) NULL,
	[Descripcion] NVARCHAR(100) NULL,
	[Simbolo] NVARCHAR(10) NULL,
	[Activo] BIT NOT NULL DEFAULT(1),
	[UpdDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	[InsDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET())
)
