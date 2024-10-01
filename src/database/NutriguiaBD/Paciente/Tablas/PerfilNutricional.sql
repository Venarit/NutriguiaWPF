CREATE TABLE [Paciente].[PerfilNutricional]
(
	[idPaciente] INT NOT NULL,
	[idUser] INT NOT NULL,
	[Peso] NUMERIC NOT NULL,
	[Altura] INT NOT NULL,
	[Sexo] NVARCHAR(50) NOT NULL,
	[Edad] INT NOT NULL,
	[BMR] INT NULL,
	[TDEE] INT NULL,
	[idActividad] INT NOT NULL,
	[idObjetivo] INT NOT NULL,
	[idMacronutrientes] INT NOT NULL,
	[Calorias] INT NOT NULL,
	[UpdDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	[InsDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	FOREIGN KEY (idPaciente) REFERENCES [Paciente].[Paciente](idPaciente),
	FOREIGN KEY (idUser) REFERENCES [User].[User](idUser),
	FOREIGN KEY (idActividad) REFERENCES [Common].[Actividad](idActividad),
	FOREIGN KEY (idObjetivo) REFERENCES [Common].[Objetivo](idObjetivo),
	FOREIGN KEY (idMacronutrientes) REFERENCES [Common].[Macronutrientes](idMacronutrientes)
)