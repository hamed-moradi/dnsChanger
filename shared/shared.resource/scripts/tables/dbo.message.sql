IF(OBJECT_ID('dbo.[message]', 'P') IS NOT NULL)
    DROP TABLE dbo.[message];
GO

/****** Object:  Table [dbo].[message]    Script Date: 5/2/2019 7:46:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[message](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Title] [nvarchar](150) NULL,
	[Email] [nvarchar](50) NULL,
	[CellPhone] [varchar](15) NULL,
	[Body] [nvarchar](max) NOT NULL,
	[IsImportant] [bit] NOT NULL,
	[UserId] [int] NULL,
	[LastSeenUserId] [int] NULL,
	[LastSeenAt] [datetime] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[message] ADD  CONSTRAINT [DF_Message_IsImportant]  DEFAULT ((0)) FOR [IsImportant]
GO
ALTER TABLE [dbo].[message] ADD  CONSTRAINT [DF_Message_LastSeenDate]  DEFAULT (getdate()) FOR [LastSeenAt]
GO
ALTER TABLE [dbo].[message] ADD  CONSTRAINT [DF_Message_CreateDate]  DEFAULT (getdate()) FOR [CreatedAt]
GO
