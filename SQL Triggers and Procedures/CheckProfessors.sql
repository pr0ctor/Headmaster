--CheckTimes Checks the Times Table to see if a Time exists within it.
--		If the Time exists it returns the existing TimeID.
--		If the Time does not exist it inserts the Time and returns the TimeID.

CREATE PROCEDURE CheckProfessors
(
	@LastName varchar(50)
)
AS
BEGIN
	DECLARE @counter int;
	DECLARE @Professor_ID int;
	
	SELECT @counter = COUNT(*) FROM Professors 
			WHERE LastName = @LastName;
			
	IF @counter > 0
		BEGIN
			SELECT @Professor_ID = ProfessorID FROM Professors 
				WHERE LastName = @LastName;
			
			RETURN @Professor_ID
		END
	ELSE
		BEGIN
			INSERT INTO Professors(LastName)
				VALUES(@LastName);

			SELECT @Professor_ID = ProfessorID FROM Professors 
				WHERE LastName = @LastName;

			RETURN @Professor_ID
		END
	
END;