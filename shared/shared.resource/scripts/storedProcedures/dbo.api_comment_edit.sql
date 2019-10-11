IF(OBJECT_ID('dbo.[api_comment_edit]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_comment_edit];
GO

/****** Object:  StoredProcedure [dbo].[api_comment_edit]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_comment_edit]
	@Token CHAR(32),
	@DeviceId VARCHAR(128),
	@Id INT NOT NULL,
	@Body NVARCHAR(MAX) NOT NULL
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL, @commentUserId INT = NULL, @commentStatus TINYINT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId;
	IF(@userStatus <> 100)
		RETURN 410; -- User is not active

	SELECT @commentUserId = UserId, @commentStatus = [Status] FROM dbo.[comment] WITH(NOLOCK) WHERE Id = @Id;
	IF(@commentStatus IS NULL)
		RETURN 415; -- Comment not found
	IF(@commentStatus <> 90 AND @commentStatus <> 100)
		RETURN 420; -- Comment is not active
	IF(@commentUserId <> @userId)
		RETURN 425; -- Access denied

	UPDATE dbo.[comment] SET [Body] = @Body, [ModifiedAt] = GETDATE() WHERE Id = @Id

	SELECT * FROM dbo.[comment] WHERE Id = @Id;
	RETURN 200;
	--BEGIN TRAN userUpdate;
	--BEGIN
	--	BEGIN TRY
	--		COMMIT TRAN userUpdate;
	--		RETURN 200; -- Done!
	--	END TRY
	--	BEGIN CATCH
	--		ROLLBACK TRAN userUpdate;
	--		RETURN 500;
	--	END CATCH
	--END;
END;