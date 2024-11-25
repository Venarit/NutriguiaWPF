CREATE PROCEDURE [Patient].[GetPatients]
AS
BEGIN
	SELECT * FROM [Patient].[Patient] WHERE Active = 1
	SELECT * FROM [Patient].[NutritionalProfile] WHERE Active = 1
	SELECT * FROM [Patient].[PatientMeasurement] WHERE Active = 1
END