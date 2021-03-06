IF(OBJECT_ID('dbo.[api_historyLog_insert]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_historyLog_insert];
GO
   
/****** Object:  StoredProcedure [dbo].[api_historyLog_insert]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_historyLog_insert]
	@UserId INT,
	@ActivityType TINYINT,
	@ActivityId BIGINT,
	@Data NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO dbo.historyLog ([UserId], [ActivityType], [ActivityId], [Data])
	VALUES (@UserId, @ActivityType, @ActivityId, @Data)
END
GO
