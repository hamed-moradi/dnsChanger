IF(OBJECT_ID('dbo.[tag]', 'P') IS NOT NULL)
    DROP TABLE dbo.[tag];
GO

/****** Object:  Table [dbo].[tag]    Script Date: 5/2/2019 7:46:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tag](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(128) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_Tag_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'Tag$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tag'
GO
