CREATE PROCEDURE [Paciente].[SetPerfilNutricional]
	@idPaciente			INT,
	@Altura				INT,
	@Sexo				NVARCHAR(1),
	@idActividad		INT,
	@idObjetivo			INT,
	@idMacronutrientes	INT
AS
BEGIN
	
	UPDATE [Paciente].[PerfilNutricional] 
	SET Activo = 0, UpdDateTime = SYSDATETIME() WHERE idPaciente = @idPaciente

	INSERT INTO [Paciente].[PerfilNutricional]
	(idPaciente, Altura, Sexo, idActividad, idObjetivo, idMacronutrientes)
	VALUES
	(@idPaciente, @Altura, @Sexo, @idActividad, @idObjetivo, @idMacronutrientes)

END
