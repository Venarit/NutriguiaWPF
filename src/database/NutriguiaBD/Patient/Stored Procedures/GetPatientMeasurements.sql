CREATE PROCEDURE [Patient].[GetPatientMeasurements]
	@IdPatient INT
AS
BEGIN
	SELECT PM.* FROM [Patient].[PatientMeasurement] PM
	LEFT JOIN [Patient].[NutritionalProfile] NP ON PM.idNutritionalProfile = NP.idNutritionalProfile
	WHERE NP.idPatient = @IdPatient
END