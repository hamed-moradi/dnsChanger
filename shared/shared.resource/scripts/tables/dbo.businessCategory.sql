IF(OBJECT_ID('dbo.[businessCategory]', 'P') IS NOT NULL)
    DROP TABLE dbo.[businessCategory];
GO

/****** Object:  Table [dbo].[businessCategory]    Script Date: 5/2/2019 7:46:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[businessCategory](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(128) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Priority] SMALLINT NOT NULL,
	[ThumbImage] NVARCHAR(512) NULL,
	[CreatedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[businessCategory] ADD  CONSTRAINT [DF_Category_ThumbImage]  DEFAULT (' ') FOR [ThumbImage]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'Category$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'businessCategory'
GO
