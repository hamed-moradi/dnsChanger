IF(OBJECT_ID('dbo.[api_product_getPaging]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_product_getPaging];
GO

/****** Object:  StoredProcedure [dbo].[api_product_getPaging]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_product_getPaging]
	@Token VARCHAR(32),
	@DeviceId VARCHAR(128),
	@BusinessId INT = NULL,
	@CategoryId INT = NULL,
	@Tags VARCHAR(128) = NULL,
	@Title NVARCHAR(128) = NULL,
	@Skip INT = 0,
	@Take INT = 10,
	@Order VARCHAR(4) = NULL,
	@OrderBy VARCHAR(32) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL, @tagIds ArrayList = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WITH(NOLOCK) WHERE Id = @userId;
	IF(@userStatus <> 90 AND @userStatus <> 100)
		RETURN 410; -- User is not active

	IF(@Tags IS NOT NULL)
		SET @tagIds = dbo.Split(@Tags);

	SELECT * FROM dbo.[product] prd WITH(NOLOCK)
	WHERE (@BusinessId IS NULL OR BusinessId = @BusinessId)
		AND (@CategoryId IS NULL OR CategoryId = @CategoryId)
		AND (@Tags IS NULL OR prd.Id IN (
			SELECT prdTag.ProductId FROM dbo.productToTag prdTag WITH(NOLOCK)
			INNER JOIN @tagIds tag ON prdTag.TagId = tag.Id AND prdTag.ProductId = prd.Id))
		AND (@Title IS NULL OR Title LIKE '%'+@Title+'%')
	ORDER BY
		CASE WHEN @OrderBy = 'Id' AND @Order = 'ASC' THEN Id END ASC,
		CASE WHEN @OrderBy = 'Id' AND (@Order <> 'ASC' OR @Order IS NULL) THEN Id END DESC,
		CASE WHEN @OrderBy = 'Title' AND @Order = 'ASC' THEN Title END ASC,
		CASE WHEN @OrderBy = 'Title' AND (@Order <> 'ASC' OR @Order IS NULL) THEN Title END DESC,
		CASE WHEN @OrderBy = 'CreatedAt' AND @Order = 'ASC' THEN CreatedAt END ASC,
		CASE WHEN @OrderBy = 'CreatedAt' AND (@Order <> 'ASC' OR @Order IS NULL) THEN CreatedAt END DESC,
		CASE WHEN @OrderBy = 'ModifiedAt' AND @Order = 'ASC' THEN ModifiedAt END ASC,
		CASE WHEN @OrderBy = 'ModifiedAt' AND (@Order <> 'ASC' OR @Order IS NULL) THEN ModifiedAt END DESC,
		CASE WHEN @OrderBy IS NULL THEN CURRENT_TIMESTAMP END
	OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

	RETURN 200;
END;
GO
