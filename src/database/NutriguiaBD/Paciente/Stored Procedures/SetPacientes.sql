CREATE PROCEDURE [Paciente].[SetPacientes]
	@Nombre				NVARCHAR(50),
	@SegundoNombre		NVARCHAR(50) = NULL,
	@ApellidoP			NVARCHAR(50),
	@ApellidoM			NVARCHAR(50) = NULL,
	@Email				NVARCHAR(50),
	@Celular			NVARCHAR(10),
	@FechaNacimiento	NVARCHAR(10)
AS
BEGIN
	
	DECLARE @Date DATE = CAST(@FechaNacimiento AS DATE)
	
	INSERT INTO [Paciente].[Paciente]
	(Nombre, SegundoNombre, Apellido_P, Apellido_M, Email, Celular, FechaNacimiento)
	VALUES
	(@Nombre, @SegundoNombre, @ApellidoP, @ApellidoM, @Email, @Celular,	@Date)

END