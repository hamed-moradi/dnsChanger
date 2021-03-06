IF(OBJECT_ID('dbo.[product]', 'P') IS NOT NULL)
    DROP TABLE dbo.[product];
GO

/****** Object:  Table [dbo].[product]    Script Date: 5/2/2019 7:46:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[BusinessId] INT NOT NULL,
	[CategoryId] INT NOT NULL,
	[Title] NVARCHAR(128) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Thumbnail] NVARCHAR(512) NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ModifiedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_ModifiedAt]  DEFAULT (GETDATE()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_Status]  DEFAULT ((10)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'User$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product'
GO
