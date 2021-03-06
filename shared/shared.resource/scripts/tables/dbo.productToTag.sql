IF(OBJECT_ID('dbo.[productToTag]', 'P') IS NOT NULL)
    DROP TABLE dbo.[productToTag];
GO

/****** Object:  Table [dbo].[productToTag]    Script Date: 5/2/2019 7:46:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productToTag](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[ProductId] INT NOT NULL,
	[TagId] INT NOT NULL,
	[CreatedAt] DATETIME NOT NULL
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[productToTag] ADD  CONSTRAINT [DF_productToTag_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'User$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'productToTag'
GO
