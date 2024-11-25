CREATE PROCEDURE [Patient].[SetPatient]
	@IdPatient INT
	,@Name NVARCHAR(50)
	,@SecondName NVARCHAR(50)
	,@LastNameP NVARCHAR(50)
	,@LastNameM NVARCHAR(50)
	,@Email NVARCHAR(50)
	,@Cellphone NVARCHAR(10)
	,@BirthDate NVARCHAR(10)
AS
BEGIN

	DECLARE @BD DATE = CAST(@BirthDate AS DATE);

	IF (@IdPatient = 0 OR @IdPatient IS NULL)
	BEGIN
		INSERT INTO [Patient].[Patient]([Name], SecondName, LastNameP, LastNameM, Email, Cellphone, BirthDate)
		VALUES (@Name, @SecondName, @LastNameP, @LastNameM, @Email, @Cellphone, @BD)
	END
	ELSE
	BEGIN
		UPDATE [Patient].[Patient]
		SET 
			[Name] = @Name
			,SecondName = @SecondName
			,LastNameP = @LastNameP
			,LastNameM = @LastNameM
			,Email = @Email
			,Cellphone = @Cellphone
			,BirthDate = @BD
			,UpdDateTime = SYSDATETIME()
		WHERE
			idPatient = @IdPatient
	END
END