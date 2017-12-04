--CheckDays Checks the Days Table to see if a Day exists within it.
--		If the Day exists it returns the existing DayID.
--		If the Day does not exist it inserts the Day and returns the DayID.

CREATE PROCEDURE CheckDays
(
	@Days varchar(50)
)
AS
BEGIN
	DECLARE @counter int;
	DECLARE @Day_ID int;
	
	SELECT @counter = COUNT(*) FROM Days 
			WHERE Days = @Day;
			
	IF @counter > 0
		BEGIN
			SELECT @Day_ID = DayID FROM Days 
				WHERE Days = @Day;
			
			RETURN @Day_ID
		END
	ELSE
		BEGIN
			INSERT INTO Days(Day)
				VALUES(@Days);

			SELECT @Day_ID = DayID FROM Days 
				WHERE Days = @Day;

			RETURN @Day_ID
		END
	
END;