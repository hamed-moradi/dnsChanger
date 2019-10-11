IF(OBJECT_ID('dbo.[api_user_disableTwoFactorAuthentication]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_user_disableTwoFactorAuthentication];
GO

/****** Object:  StoredProcedure [dbo].[api_user_disableTwoFactorAuthentication]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_user_disableTwoFactorAuthentication]
	@Token varchar(32),
	@DeviceId varchar(128),
	@Password NVARCHAR(32)
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL, @userPass NVARCHAR(32) = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userPass = [Password], @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId;
	IF(@userStatus <> 100)
		RETURN 410; -- User is not active
	IF(@userPass <> @Password)
		RETURN 415; -- Password is wrong

	UPDATE dbo.[user] SET [Password] = NULL WHERE Id = @userId;
	RETURN 200; -- Done!
END;