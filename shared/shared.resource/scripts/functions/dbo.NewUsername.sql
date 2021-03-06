IF(OBJECT_ID('dbo.[NewUsername]', 'P') IS NOT NULL)
    DROP FUNCTION dbo.[NewUsername];
GO

/****** Object:  UserDefinedFunction [dbo].[NewUsername]    Script Date: 5/2/2019 7:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[NewUsername]()
	RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @number VARCHAR(10) = NULL;
    WHILE (1 = 1)
    BEGIN
        SELECT @number = Id FROM dbo.RandomNumber WITH(NOLOCK);
        IF (LEN(@number) <> 10) CONTINUE;
        IF (EXISTS (SELECT * FROM dbo.customer WITH(NOLOCK) WHERE Username = @number)) CONTINUE;
        RETURN @number;
    END;
    RETURN @number;
END;
GO