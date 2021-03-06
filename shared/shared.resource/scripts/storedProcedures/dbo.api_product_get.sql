IF(OBJECT_ID('dbo.[api_product_get]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_product_get];
GO

/****** Object:  StoredProcedure [dbo].[api_product_get]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_product_get]
	@Token VARCHAR(32),
	@DeviceId VARCHAR(128),
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WITH(NOLOCK) WHERE Id = @userId;
	IF(@userStatus <> 90 AND @userStatus <> 100)
		RETURN 410; -- User is not active

	SELECT * FROM dbo.[product] WITH(NOLOCK) WHERE Id = @Id AND [Status] = 100; -- 100: Active
	SELECT * FROM dbo.[Image] WITH(NOLOCK) WHERE [Entity] = 'product' AND [EntityId] = @Id;

	RETURN 200;
END;
GO
