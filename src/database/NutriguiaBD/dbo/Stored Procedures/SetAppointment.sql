CREATE PROCEDURE [dbo].[SetAppointment]
	@IdAppointment INT
	,@IdPatient INT
	,@IdAppointmentStatus INT
	,@StartDateTime DATETIME
	,@EndDateTime DATETIME
	,@Notes TEXT
AS
BEGIN

	IF (@IdAppointment = 0 OR @IdAppointment IS NULL)
	BEGIN
		INSERT INTO [dbo].[Appointment](idPatient, idAppointmentStatus, StartDateTime, EndDateTime, Notes)
		VALUES (@IdPatient, @IdAppointmentStatus, @StartDateTime, @EndDateTime, @Notes)
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Appointment]
		SET 
			idPatient = @IdPatient
			,idAppointmentStatus = @IdAppointmentStatus
			,StartDateTime = @StartDateTime
			,EndDateTime = @EndDateTime
			,Notes = @Notes
			,UpdDateTime = SYSDATETIME()
		WHERE
			idAppointment = @IdAppointment
	END
END