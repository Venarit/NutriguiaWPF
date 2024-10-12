CREATE PROCEDURE [Paciente].[GetPerfilesNutricionales]
AS
BEGIN
	SELECT * FROM Paciente.PerfilNutricional WHERE Activo = 1

	SELECT * FROM Catalogo.Actividad
	WHERE idActividad IN (SELECT idActividad FROM Paciente.PerfilNutricional)

	SELECT * FROM Catalogo.Objetivo
	WHERE idObjetivo IN (SELECT idObjetivo FROM Paciente.PerfilNutricional)

	SELECT * FROM Catalogo.Macronutrientes
	WHERE idMacronutrientes IN (SELECT idMacronutrientes FROM Paciente.PerfilNutricional)

	SELECT * FROM Paciente.PacienteMedicion
	WHERE idPerfilNutricional IN (SELECT idPerfilNutricional FROM Paciente.PerfilNutricional)
END