CREATE TABLE [Paciente].[PacienteMedicion]
(
	[idPacienteMedicion] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[idPerfilNutricional] INT NOT NULL,
	[Peso] NUMERIC NOT NULL,
	[GrasaCorporal] INT NULL,
	[Calorias] INT NULL,
	[BMR] INT NULL,
	[TDEE] INT NULL,
	[Activo] BIT NOT NULL DEFAULT(1),
	[UpdDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	[InsDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	FOREIGN KEY (idPerfilNutricional) REFERENCES [Paciente].[Paciente](idPaciente)
)
