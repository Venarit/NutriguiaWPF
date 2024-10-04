CREATE PROCEDURE [Paciente].[GetPacienteMediciones]
AS
BEGIN
	SELECT * FROM Paciente.PacienteMedicion WHERE Activo = 1
END