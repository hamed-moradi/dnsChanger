IF(OBJECT_ID('dbo.[api_user_disableMe]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_user_disableMe];
GO

/****** Object:  StoredProcedure [dbo].[api_user_disableMe]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_user_disableMe]
	@Token char(32),
	@DeviceId varchar(128),
	@Description NVARCHAR(MAX) = NULL
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WITH(NOLOCK) WHERE Id = @userId;
	IF(@userStatus <> 100)
		RETURN 410; -- User is not active

	BEGIN TRAN userDisableMe;
	BEGIN
		BEGIN TRY
			UPDATE dbo.[user] SET [Status] = 90 WHERE Id = @userId; -- 90: Disabled by itself
			UPDATE dbo.[customer] SET [Status] = 90 WHERE UserId = @userId; -- 90: Disabled by user itself
			UPDATE dbo.[business] SET [Status] = 90 WHERE UserId = @userId; -- 90: Disabled by user itself
			COMMIT TRAN userDisableMe;
			RETURN 200; -- Done!
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN userDisableMe;
			RETURN 500;
		END CATCH
	END;
END;