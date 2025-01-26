CREATE PROCEDURE [dbo].[GetAppointmentHistory]
	@BeginDateTime DATETIME
	,@EndDateTime DATETIME
AS
BEGIN

	SELECT * FROM [dbo].[Appointment]
	WHERE StartDateTime BETWEEN @BeginDateTime AND @EndDateTime

	SELECT DISTINCT S.* FROM [Catalog].[AppointmentStatus] S
	RIGHT JOIN [dbo].[Appointment] A ON A.idAppointmentStatus = S.idAppointmentStatus

	SELECT DISTINCT P.* FROM Patient.Patient P
	RIGHT JOIN [dbo].[Appointment] A ON P.idPatient = A.idPatient

END