IF(OBJECT_ID('dbo.[page]', 'P') IS NOT NULL)
    DROP TABLE dbo.[page];
GO

/****** Object:  Table [dbo].[page]    Script Date: 5/2/2019 7:46:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[page](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueName] [varchar](50) NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[UpTitle] [nvarchar](256) NOT NULL,
	[SubTitle] [nvarchar](256) NOT NULL,
	[Summary] [nvarchar](max) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[ThumbImage] [nvarchar](500) NOT NULL,
	[BannerImage] [nvarchar](500) NOT NULL,
	[CreatorId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdaterId] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Page] UNIQUE NONCLUSTERED 
(
	[UniqueName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[page] ADD  CONSTRAINT [DF_Page_UpTitle]  DEFAULT ('') FOR [UpTitle]
GO
ALTER TABLE [dbo].[page] ADD  CONSTRAINT [DF_Page_SubTitle]  DEFAULT ('') FOR [SubTitle]
GO
ALTER TABLE [dbo].[page] ADD  CONSTRAINT [DF_Page_Summary]  DEFAULT ('') FOR [Summary]
GO
