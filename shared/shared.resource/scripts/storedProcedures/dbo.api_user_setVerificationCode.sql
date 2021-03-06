IF(OBJECT_ID('dbo.[api_user_setVerificationCode]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_user_setVerificationCode];
GO

/****** Object:  StoredProcedure [dbo].[api_user_setVerificationCode]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_user_setVerificationCode]
	@DeviceId VARCHAR(128),
	@CellPhone VARCHAR(16),
	@Email VARCHAR(64),
	@VerificationCode VARCHAR(64)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @userId INT = NULL,	@userStatus TINYINT = NULL, @sessionStatus TINYINT = NULL, @userPropId INT = NULL,
		@userEmail VARCHAR(64) = NULL, @userCellPhone VARCHAR(16);

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE DeviceId = @DeviceId
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Device not found

	--SELECT @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId
	--IF(@userStatus <> 100)
	--	RETURN 410; -- User is not active

	IF(@CellPhone IS NOT NULL)
	BEGIN
		SELECT @userCellPhone = [Value] FROM dbo.userproperty WITH(NOLOCK) WHERE UserId = @userId AND PropTypeId = 2 -- 2: CellPhone
		IF(@userCellPhone IS NULL)
			RETURN 416; -- You must register a cell phone first
		IF(@userCellPhone <> @CellPhone)
			RETURN 417; -- CellPhone does not match
	END

	IF(@Email IS NOT NULL)
	BEGIN
		SELECT @userEmail = [Value] FROM dbo.userproperty WITH(NOLOCK) WHERE UserId = @userId AND PropTypeId = 3 -- 2: Email
		IF(@userEmail IS NULL)
			RETURN 418; -- You must register an email first
		IF(@userEmail <> @Email)
			RETURN 419; -- Email does not match
	END

	SELECT @userPropId = Id FROM dbo.userproperty WITH(NOLOCK) WHERE UserId = @userId AND PropTypeId = 5 -- 5: VerificationCode
	IF(@userPropId IS NULL)
		INSERT INTO dbo.userProperty(UserId, PropTypeId, [Value]) VALUES (@userId, 5, @VerificationCode); -- 5: VerificationCode
	ELSE
		UPDATE dbo.userProperty SET [Value] = @VerificationCode, ModifiedAt = GETDATE() WHERE Id = @userPropId

	RETURN 200; -- Verification code has been set
END
GO
