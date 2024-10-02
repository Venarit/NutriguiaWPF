CREATE PROCEDURE [Paciente].[GetPacientes]
AS
BEGIN
	SELECT
		idPaciente,
		Nombre,
		SegundoNombre,
		Apellido_P,
		Apellido_M,
		Email,
		Celular,
		CAST(FechaNacimiento AS NVARCHAR) AS FechaNacimiento
	FROM [Paciente].[Paciente]
END