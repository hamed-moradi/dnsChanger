IF(OBJECT_ID('dbo.[api_changeLog_insert]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_changeLog_insert];
GO
   
/****** Object:  StoredProcedure [dbo].[api_changeLog_insert]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_changeLog_insert]
	@AdminId INT,
	@ActionType TINYINT,
	@Entity VARCHAR(32),
	@EntityId BIGINT,
	@Data NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO dbo.changeLog ([AdminId], [ActionType], [Entity], [EntityId], [Data])
	VALUES (@AdminId, @ActionType, @Entity, @EntityId, @Data)
END
GO
