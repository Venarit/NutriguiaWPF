CREATE TABLE [Paciente].[PerfilNutricional]
(
	[idPerfilNutricional] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[idPaciente] INT NOT NULL,
	--[idUsuario] INT NOT NULL,			-- Mover a paciente.paciente?
	--[Peso] NUMERIC NOT NULL,			-- Variable, agrega a un historico
	[Altura] INT NOT NULL,				-- Constante
	[Sexo] NVARCHAR(50) NOT NULL,		-- Constante
	--[Edad] INT NOT NULL,				-- Variable (eliminar columna, no necesita ser guardado teniendo fecha nacimiento en paciente.paciente)
	--[BMR] INT NULL,					-- Variable
	--[TDEE] INT NULL,					-- Variable
	[idActividad] INT NOT NULL,			-- Variable Editable
	[idObjetivo] INT NOT NULL,			-- Variable Editable
	[idMacronutrientes] INT NOT NULL,	-- Variable Editable
	--[Calorias] INT NOT NULL,			-- Variable según peso, agregar a histórico
	[Activo] BIT NOT NULL DEFAULT(1),
	[UpdDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	[InsDateTime] DATETIMEOFFSET(7) NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	FOREIGN KEY (idPaciente) REFERENCES [Paciente].[Paciente](idPaciente),
	--FOREIGN KEY (idUsuario) REFERENCES [Seguridad].[Usuario](idUsuario),
	FOREIGN KEY (idActividad) REFERENCES [Catalogo].[Actividad](idActividad),
	FOREIGN KEY (idObjetivo) REFERENCES [Catalogo].[Objetivo](idObjetivo),
	FOREIGN KEY (idMacronutrientes) REFERENCES [Catalogo].[Macronutrientes](idMacronutrientes)
)