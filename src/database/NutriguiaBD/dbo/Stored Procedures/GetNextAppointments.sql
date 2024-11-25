CREATE PROCEDURE [dbo].[GetNextAppointments]
AS
BEGIN
	SELECT TOP 3 * FROM [dbo].[Appointment] 
	WHERE 
		StartDateTime > SYSDATETIME()
		AND idAppointmentStatus NOT IN (3,4) --CANCELADA O CONFIRMADA
	ORDER BY 
		StartDateTime
END
