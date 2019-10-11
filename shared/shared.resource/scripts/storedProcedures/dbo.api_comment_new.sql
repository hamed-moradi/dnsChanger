IF(OBJECT_ID('dbo.[api_comment_new]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_comment_new];
GO

/****** Object:  StoredProcedure [dbo].[api_comment_new]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_comment_new]
	@Token CHAR(32),
	@DeviceId VARCHAR(128),
	@ParentId INT = NULL,
	@EntityId INT,
	@Entity VARCHAR(32),
	@Body NVARCHAR(MAX)
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL, @commentId INT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId;
	IF(@userStatus <> 100)
		RETURN 410; -- User is not active

	INSERT INTO dbo.[comment] ([UserId], [ParentId], [EntityId], [Entity], [Body])
	VALUES (@userId, @ParentId, @EntityId, @Entity, @Body)
	SET @commentId = SCOPE_IDENTITY();

	SELECT * FROM dbo.[comment] WHERE Id = @commentId;
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