IF(OBJECT_ID('dbo.[api_business_new]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_business_new];
GO

/****** Object:  StoredProcedure [dbo].[api_business_new]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_business_new]
	@Token char(32),
	@DeviceId varchar(128),
	@Title NVARCHAR(128),
	@Description NVARCHAR(MAX) = NULL,
	@Address NVARCHAR(512) = NULL,
	@BusinessCode VARCHAR(16),
	@ThumbImage NVARCHAR(512) = NULL,
	@Latitude FLOAT,
	@Longitude FLOAT,
	@MakeItValid BIT = 0
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL, @point GEOGRAPHY = NULL, @businessId INT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId;
	IF(@userStatus <> 100)
		RETURN 410; -- User is not active

	SET @point = geography::Point(@Latitude, @Longitude, 4326);
	IF(@point.STIsValid() <> 1)
		IF(@MakeItValid = 1)
			SET @point = @point.MakeValid();
		ELSE
			RETURN 411;
	
	INSERT INTO dbo.[business] ([CustomerId], [Title], [Description], [Address], [BusinessCode], [ThumbImage], [Latitude], [Longitude], [Point])
	VALUES (@userId, @Title, @Description, @Address, @BusinessCode, @ThumbImage, @Latitude, @Longitude, @point)
	SET @businessId = SCOPE_IDENTITY();

	SELECT * FROM dbo.[business] WHERE Id = @businessId;
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