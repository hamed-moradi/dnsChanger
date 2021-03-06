IF(OBJECT_ID('dbo.[comment]', 'P') IS NOT NULL)
    DROP TABLE dbo.[comment];
GO

/****** Object:  Table [dbo].[comment]    Script Date: 5/2/2019 7:46:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UserId] int NOT NULL,
	[ParentId] INT NULL,
	[EntityId] BIGINT NOT NULL,
	[Entity] VARCHAR(32) NOT NULL,
	[Body] NVARCHAR(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ModifiedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[business] ADD  CONSTRAINT [DF_business_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[business] ADD  CONSTRAINT [DF_business_ModifiedAt]  DEFAULT (GETDATE()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[business] ADD  CONSTRAINT [DF_business_Status]  DEFAULT ((10)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'Comment$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'comment'
GO
