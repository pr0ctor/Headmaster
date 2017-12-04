--InsertAvailableCourse Takes all the elements of an Available Course and insets them into the Database.
	--The Procedure takes all the parts and checks them against the tables they are stored in.
	--It retrieves all of the ids for the elements.
	--It then runs an insert statement to add the Available Course to the table.


CREATE PROCEDURE InsertAvailableCourse
(
	@CourseReferenceNumber varchar(50),
	@Department varchar(50),
	@CourseNumber varchar(50),
	@Section varchar(25),
	@CourseName varchar(50),
	@Abbreviation varchar(50),
	@BuildingName varchar(50),
	@RoomNumber varchar(10),
	@ProfessorName varchar(50),
	@Days varchar(50),
	@Times varchar(50),
	@SemesterYearID int
)
AS
BEGIN
	DECLARE @Course_ID int;
	DECLARE @Department_ID int;
	DECLARE @Building_ID int;
	DECLARE @Professor_ID int;
	DECLARE @Days_ID int;
	DECLARE @Times_ID int;

	--***Check Department Before Course***

	--Call CheckDepartments

	EXECUTE @Department_ID = CheckDepartment @Department;

	--Call CheckCourses

	EXECUTE @Course_ID = CheckCourse @Department_ID,@CourseName,@CourseNumber;

	--Call CheckProfessors

	EXECUTE @Professor_ID = CheckProfessors @ProfessorName;

	--Call CheckBuildings

	EXECUTE @Building_ID = CheckBuildings @Abbreviation,@BuildingName;

	--Call CheckDays

	EXECUTE @Days_ID = CheckDays @Days;

	--Call CheckTimes

	EXECUTE @Times_ID = CheckTimes @Times;

	--All the IDs should be populated now.
	--Insert into Available courses.

	INSERT INTO AvailableCourses(CourseID,Section,CRN,TimeID,DayID,BuildingID,ProfessorID,SemesterYearID,RoomNumber)
		VALUES(@Course_ID,@Section,@CourseReferenceNumber,@Times_ID,@Days_ID,@Building_ID,@Professor_ID,@SemesterYearID,@RoomNumber);


END;