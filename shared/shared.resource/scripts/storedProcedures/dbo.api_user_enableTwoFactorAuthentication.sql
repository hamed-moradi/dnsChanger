IF(OBJECT_ID('dbo.[api_user_enableTwoFactorAuthentication]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_user_enableTwoFactorAuthentication];
GO

/****** Object:  StoredProcedure [dbo].[api_user_enableTwoFactorAuthentication]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_user_enableTwoFactorAuthentication]
	@Token varchar(32),
	@DeviceId varchar(128),
	@VerificationCode int,
	@Password NVARCHAR(32)
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @verifyCode INT = NULL, @verifyCodeModifiedAt DATETIME = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	IF(100 NOT IN (SELECT [Status] FROM dbo.[user] WHERE Id = @userId))
		RETURN 410; -- User is not active

	SELECT @verifyCode = [Value], @verifyCodeModifiedAt = ModifiedAt FROM dbo.userProperty WITH(NOLOCK) WHERE UserId = @userId AND PropTypeId = 5 -- 5: Verification Code
	IF(@verifyCode IS NULL)
		RETURN 411; -- User must request for a verification code before enabling two step authentication
	IF(DATEDIFF(MINUTE, @verifyCodeModifiedAt, GETDATE()) > 10) -- 10 minutes!
		RETURN 412; -- Your code has expired
	IF(@verifyCode <> @VerificationCode)
		RETURN 413; -- Verification code is not valid

	UPDATE dbo.[user] SET [Password] = @Password WHERE Id = @userId
	RETURN 200; -- Done!
END;