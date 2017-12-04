--CheckBuildings Checks the Buildings Table to see if a Building exists within it.
--		If the Building exists it returns the existing BuidlingID.
--		If the Building does not exist it inserts the Building and returns the BuildingID.

CREATE PROCEDURE CheckBuildings
(
	@Abbreviation varchar(50),
	@BuildingName varchar(50)
)
AS
BEGIN
	DECLARE @counter int;
	DECLARE @Building_ID int;
	
	SELECT @counter = COUNT(*) FROM Buildings 
			WHERE Abbreviaion = @Abbreviation AND BuidingName = @BuildingName;
			
	IF @counter > 0
		BEGIN
			SELECT @Building_ID = BuildingID FROM Buildings 
				WHERE Abbreviaion = @Abbreviation AND BuidingName = @BuildingName;
			
			RETURN @Building_ID
		END
	ELSE
		BEGIN
			INSERT INTO Buildings(Abbreviaion,BuidingName)
				VALUES(@Abbreviation,@BuildingName);

			SELECT @Building_ID = BuildingID FROM Buildings 
				WHERE Abbreviaion = @Abbreviation AND BuidingName = @BuildingName;

			RETURN @Building_ID
		END
	
END;