CREATE PROCEDURE [dbo].[spAddDiagnostic]
	@operation varchar(10),
	@result int
AS
BEGIN
	INSERT INTO Diagnostics (Operation, Result)
		VALUES (@operation, @result)
END
