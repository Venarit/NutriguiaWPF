CREATE PROCEDURE [Patient].[GetPatientAppointments]
	@IdPatient INT
AS
BEGIN
	SELECT * FROM [dbo].[Appointment] 
	WHERE idPatient = @IdPatient

	SELECT DISTINCT S.* FROM [dbo].[Appointment] A
	LEFT JOIN [Catalog].[AppointmentStatus] S ON A.idAppointmentStatus = S.idAppointmentStatus 
	WHERE A.idPatient = @IdPatient
END