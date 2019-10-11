IF(OBJECT_ID('dbo.[api_business_edit]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_business_edit];
GO

/****** Object:  StoredProcedure [dbo].[api_business_edit]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_business_edit]
	@Token char(32),
	@DeviceId varchar(128),
	@Id INT,
	@Title NVARCHAR(128) = NULL,
	@Description NVARCHAR(MAX) = NULL,
	@Address NVARCHAR(512) = NULL,
	@BusinessCode VARCHAR(16) = NULL,
	@Thumbnail NVARCHAR(512) = NULL,
	@Latitude FLOAT = NULL,
	@Longitude FLOAT = NULL,
	@MakeItValid BIT = 0,
	@Status TINYINT = NULL
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL, @point GEOGRAPHY = NULL, @businessStatus TINYINT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId;
	IF(@userStatus <> 100)
		RETURN 410; -- User is not active

	SELECT @businessStatus = [Status] FROM dbo.[business] WHERE Id = @Id;
	IF(@businessStatus IS NULL)
		RETURN 415; -- Business not found
	IF(@businessStatus <> 90 AND @businessStatus <> 100)
		RETURN 420; -- Business is not active

	IF(@Latitude IS NOT NULL AND @Longitude IS NOT NULL)
	BEGIN
		SET @point = geography::Point(@Latitude, @Longitude, 4326);
		IF(@point.STIsValid() <> 1)
			IF(@MakeItValid = 1)
				SET @point = @point.MakeValid();
			ELSE
				RETURN 411;
	END;
	
	UPDATE dbo.[business] SET [Title] = ISNULL(@Title, [Title]), [Description] = ISNULL(@Description, [Description]), [Address] = ISNULL(@Address, [Address]),
		[BusinessCode] = ISNULL(@BusinessCode, [BusinessCode]), [Thumbnail] = ISNULL(@Thumbnail, [Thumbnail]), [Latitude] = ISNULL(@Latitude, [Latitude]),
		[Longitude] = ISNULL(@Longitude, [Longitude]), [Point] = ISNULL(@point, [Point]), [Status] = ISNULL(@Status, [Status])
	WHERE Id = @Id;

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