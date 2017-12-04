--CheckCourse Checks the Course Table to see if a course exists within it.
--		If the course exists it returns the existing CourseID.
--		If the course does not exist it inserts the course and returns the CourseID.

CREATE PROCEDURE CheckCourse
(
	@Dept_ID int,
	@Course_Name varchar(250),
	@Course_Number varchar(50)
)
AS
BEGIN
	DECLARE @counter int;
	DECLARE @Course_ID int;
	
	SELECT @counter = COUNT(*) FROM Courses 
			WHERE CourseName = @Course_Name AND DepartmentID = @Dept_ID AND CourseNumber = @Course_Number;
			
	IF @counter > 0
		BEGIN
			SELECT @Course_ID = CourseID FROM Courses 
				WHERE CourseName = @Course_Name AND DepartmentID = @Dept_ID AND CourseNumber = @Course_Number;
			
			RETURN @Course_ID
		END
	ELSE
		BEGIN
			INSERT INTO Courses(DepartmentID,CourseName,CourseNumber)
				VALUES(@Dept_ID,@Course_Name,@Course_Number);

			SELECT @Course_ID = CourseID FROM Courses 
				WHERE CourseName = @Course_Name AND DepartmentID = @Dept_ID AND CourseNumber = @Course_Number;
			
			RETURN @Course_ID
		END
	
END;