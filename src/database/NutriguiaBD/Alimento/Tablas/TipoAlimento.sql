CREATE TABLE [Alimento].[TipoAlimento]
(
	[idTipoAlimento] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Codigo] NVARCHAR(100) NOT NULL,
	[Nombre] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(100) NOT NULL,
	[Activo] BIT NOT NULL DEFAULT(1),
	[UpdDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	[InsDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET())
)

--CREATE TRIGGER [Alimento].[Update_DateTime]
--ON [Alimento].[TipoAlimento]
--AFTER UPDATE 
--AS
--BEGIN
--	SET NOCOUNT ON;
--	UPDATE [TipoAlimento]
--	SET [UpdDateTime] = CURRENT_TIMESTAMP
--END;
--GO

--ALTER TABLE [Alimento].[TipoAlimento] ENABLE TRIGGER [Update_DateTime]
--GO