IF(OBJECT_ID('dbo.[api_product_edit]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_product_edit];
GO

/****** Object:  StoredProcedure [dbo].[api_product_edit]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_product_edit]
	@Token char(32),
	@DeviceId varchar(128),
	@Id INT NOT NULL,
	@CategoryId INT NOT NULL,
	@Title NVARCHAR(128) NOT NULL,
	@Description NVARCHAR(MAX) = NULL,
	@Thumbnail NVARCHAR(512) NOT NULL,
	@Images dbo.[image] READONLY
AS
BEGIN
	DECLARE @userId INT = NULL, @sessionStatus TINYINT = NULL, @userStatus TINYINT = NULL, @productStatus INT = NULL;

	SELECT @userId = UserId, @sessionStatus = [Status] FROM dbo.[session] WITH(NOLOCK) WHERE Token = @Token AND DeviceId = @DeviceId;
	IF(@sessionStatus IS NULL)
		RETURN 400; -- Authentication failed
	IF(@sessionStatus <> 100)
		RETURN 405; -- Device is not active

	SELECT @userStatus = [Status] FROM dbo.[user] WHERE Id = @userId;
	IF(@userStatus <> 100)
		RETURN 410; -- User is not active

	SELECT @productStatus = [Status] FROM dbo.[product] WITH(NOLOCK) WHERE Id = @Id
	IF(@productStatus IS NULL)
		RETURN 415; -- Product not found
	IF(@productStatus <> 90 AND @productStatus <> 100)
		RETURN 420; -- Product is not active

	BEGIN TRAN productNew;
	BEGIN
		BEGIN TRY
			-- Edit product
			UPDATE dbo.[product] SET [CategoryId] = ISNULL(@CategoryId, [CategoryId]), [Title] = ISNULL(@Title, [Title]),
				[Description] = ISNULL(@Description, [Description]), [Thumbnail] = ISNULL(@Thumbnail, [Thumbnail])
			WHERE Id = @Id

			-- Edit product images
			IF(EXISTS(SELECT 1 FROM @Images))
			BEGIN
				INSERT INTO dbo.[image]([EntityId], [ScaleId], [Entity], [Name], [Extension], [Path], [Description])
				SELECT @Id, [ScaleId], 'product', [Name], [Extension], [Path], [Description] FROM @Images
				WHERE NOT EXISTS (SELECT 1 FROM dbo.[image] WITH(NOLOCK) WHERE @Images.Id = [image].Id);

				UPDATE dbo.[image] SET [ScaleId] = @Images.[ScaleId], [Name] = @Images.[Name], [Extension] = @Images.[Extension],
					[Path] = @Images.[Path], [Description] = @Images.[Description]
				FROM @Image
				INNER JOIN dbo.[image] img WITH(NOLOCK) ON @Image.Id = img.Id;

				DELETE FROM dbo.[image] WHERE NOT EXISTS(SELECT 1 FROM @Image
					INNER JOIN dbo.[image] img WITH(NOLOCK) ON @Image.Id = img.Id);

				--MERGE dbo.[image] AS trg
				--USING @Images AS src
				--ON (src.Id = trg.Id)
				--WHEN MATCHED THEN
				--	UPDATE SET trg.[ScaleId] = src.[ScaleId], trg.[Name] = src.[Name], trg.[Extension] = src.[Extension], trg.[Path] = src.[Path], trg.[Description] = src.[Description]
				--WHEN NOT MATCHED BY TARGET THEN
				--	INSERT ([EntityId], [ScaleId], [Entity], [Name], [Extension], [Path], [Description])
				--	VALUES (@Id, src.[ScaleId], 'product', src.[Name], src.[Extension], src.[Path], src.[Description])
				--WHEN NOT MATCHED BY SOURCE THEN
				--	DELETE;
			END;

			COMMIT TRAN productNew;

			SELECT * FROM dbo.[product] WHERE Id = @productId;
			SELECT * FROM dbo.[image] WHERE [Entity] = 'product' AND EntityId = @productId;
			RETURN 200; -- Done!
		END TRY
		BEGIN CATCH
			ROLLBACK TRAN productNew;
			RETURN 500;
		END CATCH
	END;
END;