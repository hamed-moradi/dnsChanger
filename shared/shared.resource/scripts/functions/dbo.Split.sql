IF(OBJECT_ID('dbo.[Split]', 'P') IS NOT NULL)
    DROP FUNCTION dbo.[Split];
GO

/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 5/2/2019 7:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[Split] (@String VARCHAR(MAX), @Delimiter CHAR(1) = NULL)
RETURNS
	@ReturnList TABLE ([Item] NVARCHAR(MAX) NULL)
AS
BEGIN
	DECLARE @name NVARCHAR(MAX), @pos INT;
	IF(@Delimiter IS NULL)
		SET @Delimiter = ',';

	WHILE CHARINDEX(@Delimiter, @String) > 0
	BEGIN
		SELECT @pos  = CHARINDEX(@Delimiter, @String);
		SELECT @name = SUBSTRING(@String, 1, @pos-1);

		INSERT INTO @ReturnList SELECT @name;

		SELECT @String = SUBSTRING(@String, @pos+1, LEN(@String)-@pos);
	END

	INSERT INTO @ReturnList SELECT @String;

	RETURN;
END
GO
