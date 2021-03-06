IF(OBJECT_ID('dbo.[api_exception_insert]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_exception_insert];
GO
   
/****** Object:  StoredProcedure [dbo].[api_exception_insert]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_exception_insert]
	@Data NVARCHAR(MAX),
	@Message NVARCHAR(MAX),
	@Source NVARCHAR(MAX),
	@StackTrace NVARCHAR(MAX),
	@TargetSite NVARCHAR(MAX) = NULL,
	@URL NVARCHAR(MAX),
	@IP NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO dbo.exception ([Data], [Message], [Source], StackTrace, TargetSite, [URL], [IP])
	VALUES (@Data, @Message, @Source, @StackTrace, @TargetSite, @URL, @IP)
END
GO
