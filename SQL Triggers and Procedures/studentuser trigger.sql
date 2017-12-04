CREATE TRIGGER CreateStudent ON [AspNetUsers]
FOR INSERT
AS DECLARE @UserID nvarchar(128);
	
SELECT @UserID = ins.UserID FROM INSERTED ins;

INSERT INTO [Students]([UserID]) VALUES(@UserID);

GO