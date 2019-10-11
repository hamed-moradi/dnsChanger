IF(OBJECT_ID('dbo.[api_user_update]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_user_update];
GO

/****** Object:  StoredProcedure [dbo].[api_user_update]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_user_update]
	@Token char(32),
	@DeviceId varchar(128),
	@Avatar NVARCHAR(512) = NULL,
	@Nickname NVARCHAR(64) = NULL,
	@BirthDate DATETIME = NULL
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId;
	IF(@userStatus <> 100)
		RETURN 410; -- User is not active

	BEGIN TRAN userUpdate;
	BEGIN
		BEGIN TRY
			UPDATE dbo.[user] SET [Nickname] = @Nickname, [Avatar] = @Avatar WHERE Id = @userId;
			IF(@BirthDate IS NOT NULL) --  AND EXISTS(SELECT 1 FROM dbo.[customer] WITH(NOLOCK) WHERE UserId = @userId)
				UPDATE dbo.[customer] SET BirthDate = @BirthDate WHERE UserId = @userId;
			COMMIT TRAN userUpdate;
			RETURN 200; -- Done!
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN userUpdate;
			RETURN 500;
		END CATCH
	END;
END;