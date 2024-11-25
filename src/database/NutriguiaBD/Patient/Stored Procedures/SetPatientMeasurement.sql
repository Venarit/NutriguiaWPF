CREATE PROCEDURE [Patient].[SetPatientMeasurement]
	@IdNutritionalProfile INT
	,@Weight DECIMAL(10,2)
	,@BodyFat DECIMAL(10,2)
	,@BMR INT
	,@TDEE INT
AS
BEGIN

	IF (EXISTS (SELECT * FROM Patient.PatientMeasurement WHERE idNutritionalProfile = @IdNutritionalProfile AND Active = 1))
	BEGIN
		UPDATE Patient.PatientMeasurement SET Active = 0, UpdDateTime = SYSDATETIME()
		WHERE idNutritionalProfile = @IdNutritionalProfile AND Active = 1
	END
	
	INSERT INTO [Patient].[PatientMeasurement](idNutritionalProfile, [Weight], BodyFat, BMR, TDEE, Active)
	VALUES (@IdNutritionalProfile, @Weight, @BodyFat, @BMR, @TDEE, 1)
END