CREATE PROCEDURE [Patient].[SetNutritionalProfile]
	@IdPatient INT
	,@Height INT
	,@Sex NVARCHAR(1)
	,@idActivity INT
	,@idObjective INT
	,@idMacronutrients INT
AS
BEGIN

	IF (EXISTS (SELECT * FROM Patient.NutritionalProfile WHERE idPatient = @IdPatient AND Active = 1))
	BEGIN
		UPDATE [Patient].[NutritionalProfile] SET Active = 0, UpdDateTime = SYSDATETIME()
		WHERE idPatient = @IdPatient AND Active = 1
	END
	
	INSERT INTO [Patient].[NutritionalProfile](idPatient, Height, Sex, idActivity, idObjective, idMacronutrients, Active)
	VALUES (@IdPatient, @Height, @Sex, @idActivity, @idObjective, @idMacronutrients, 1)
END