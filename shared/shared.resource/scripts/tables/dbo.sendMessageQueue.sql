IF(OBJECT_ID('dbo.[sendMessageQueue]', 'P') IS NOT NULL)
    DROP TABLE dbo.[sendMessageQueue];
GO

/****** Object:  Table [dbo].[sendMessageQueue]    Script Date: 5/2/2019 7:46:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sendMessageQueue](
	[Id] BIGINT IDENTITY(1,1) NOT NULL,
	[TypeId] TINYINT NOT NULL,
	[CategoryId] TINYINT NOT NULL,
	[CellPhone] VARCHAR(16) NULL,
	[Email] VARCHAR(64) NULL,
	[Subject] NVARCHAR(512) NULL,
	[Body] NVARCHAR(MAX) NOT NULL,
	[Gateway] VARCHAR(32) NOT NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[ModifiedAt] DATETIME NOT NULL DEFAULT GETDATE(),
	[Status] TINYINT NOT NULL DEFAULT 100,
 CONSTRAINT [PK_SendQueue] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'صف ارسال پیام ها' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sendMessageQueue'
GO
