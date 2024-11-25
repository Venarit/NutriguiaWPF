CREATE PROCEDURE [dbo].[GetAppointments]
	@Date DATETIME = NULL
AS
BEGIN
	SELECT
		idAppointment
		,idPatient
		,idAppointmentStatus
		,StartDateTime
		,EndDateTime
		,Notes
		,Active
		,UpdDateTime
		,InsDateTime
	FROM [dbo].[Appointment] WHERE (@Date IS NULL OR @Date = (CONVERT(date,[StartDateTime]))) ORDER BY StartDateTime

	SELECT DISTINCT S.* FROM [Catalog].[AppointmentStatus] S
	RIGHT JOIN [dbo].[Appointment] A ON A.idAppointmentStatus = S.idAppointmentStatus

	SELECT DISTINCT P.* FROM Patient.Patient P
	RIGHT JOIN [dbo].[Appointment] A ON P.idPatient = A.idPatient
END