CREATE PROCEDURE [Patient].[GetPatientDislikedFood]
	@IdPatient INT
AS
BEGIN
	SELECT F.* FROM [Food].[Food] F
	JOIN [Patient].[PatientDislikedFood] DF ON F.idFood = DF.idFood
	WHERE DF.idPatient = @IdPatient
END