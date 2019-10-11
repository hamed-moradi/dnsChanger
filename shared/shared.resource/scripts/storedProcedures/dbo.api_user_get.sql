IF(OBJECT_ID('dbo.[api_user_get]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_user_get];
GO

/****** Object:  StoredProcedure [dbo].[api_user_get]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_user_get]
	@Token varchar(32),
	@DeviceId varchar(128)
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId;
	IF(@userStatus <> 90 AND @userStatus <> 100)
		RETURN 410; -- User is not active

	SELECT * FROM dbo.[user] WITH(NOLOCK) WHERE Id = @userId;
	SELECT * FROM dbo.[customer] WITH(NOLOCK) WHERE UserId = @userId
	SELECT * FROM dbo.[userProperty] WITH(NOLOCK) WHERE UserId = @userId
		AND [Status] IN (10, 90, 100) -- 10: Pendding, 90: Disabled by itself, 100: Active
		AND PropTypeId IN (2, 3); -- 2: CellPhone, 3: Email

	RETURN 200;
END;