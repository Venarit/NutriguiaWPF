CREATE PROCEDURE [Patient].[SetPatientDislikedFood]
	@IdPatient INT
	,@IdFood INT
AS
BEGIN
	IF EXISTS(SELECT * FROM [Patient].[PatientDislikedFood] WHERE idPatient = @IdPatient AND idFood = @IdFood)
	BEGIN
		DELETE FROM [Patient].[PatientDislikedFood] WHERE idPatient = @IdPatient AND idFood = @IdFood
	END
	ELSE
	BEGIN
		INSERT INTO [Patient].[PatientDislikedFood](idPatient, idFood, Active)
		VALUES (@IdPatient, @IdFood, 1)
	END
END