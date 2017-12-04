--CheckTimes Checks the Times Table to see if a Time exists within it.
--		If the Time exists it returns the existing TimeID.
--		If the Time does not exist it inserts the Time and returns the TimeID.

CREATE PROCEDURE CheckTimes
(
	@Times varchar(50)
)
AS
BEGIN
	DECLARE @counter int;
	DECLARE @Time_ID int;
	
	SELECT @counter = COUNT(*) FROM Times 
			WHERE Times = @Times;
			
	IF @counter > 0
		BEGIN
			SELECT @Time_ID = TimeID FROM Times 
				WHERE Times = @Times;
			
			RETURN @Time_ID
		END
	ELSE
		BEGIN
			INSERT INTO Times(Times)
				VALUES(@Times);

			SELECT @Time_ID = TimeID FROM Times 
				WHERE Times = @Times;

			RETURN @Time_ID
		END
	
END;