--CheckDepartment Checks the Departments Table to see if a Department Abbreviation exists within it.
--		If the Abbreviation exists it returns the existing DepartmentID.
--		If the Abbreviation does not exist it inserts the Depatrment and returns the DepartmentID.

CREATE PROCEDURE CheckDepartment
(
	@Abbreviation varchar(50)
)
AS
BEGIN
	DECLARE @counter int;
	DECLARE @Dept_ID int;
	
	SELECT @counter = COUNT(*) FROM Departments 
			WHERE Abbreviation = @Abbreviation;
			
	IF @counter > 0
		BEGIN
			SELECT @Dept_ID = DepartmentID FROM Departments 
				WHERE Abbreviation = @Abbreviation;
			
			RETURN @Dept_ID
		END
	ELSE
		BEGIN
			INSERT INTO Departments(Abbreviation)
				VALUES(@Abbreviation);

			SELECT @Dept_ID = DepartmentID FROM Departments 
				WHERE Abbreviation = @Abbreviation;

			RETURN @Dept_ID
		END
	
END;