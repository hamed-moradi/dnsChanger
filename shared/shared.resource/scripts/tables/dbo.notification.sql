IF(OBJECT_ID('dbo.[notification]', 'P') IS NOT NULL)
    DROP TABLE dbo.[notification];
GO

/****** Object:  Table [dbo].[notification]    Script Date: 5/2/2019 7:46:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[UserCount] [int] NOT NULL,
	[CreatorId] [int] NULL,
	[CreatorUserId] [int] NULL,
	[CreatedAt] [smalldatetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
