CREATE PROCEDURE [Paciente].[GetPerfilNutricional]
AS
BEGIN
	SELECT * FROM Paciente.PerfilNutricional WHERE Activo = 1
END