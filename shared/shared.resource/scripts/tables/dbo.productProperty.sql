IF(OBJECT_ID('dbo.[productProperty]', 'P') IS NOT NULL)
    DROP TABLE dbo.[productProperty];
GO

/****** Object:  Table [dbo].[productProperty]    Script Date: 5/2/2019 7:46:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productProperty](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[ProductId] INT NOT NULL,
	[TypeId] SMALLINT NOT NULL,
	[Key] NVARCHAR(512) NOT NULL,
	[Value] NVARCHAR(MAX) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ModifiedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[productProperty] ADD  CONSTRAINT [DF_productProperty_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[productProperty] ADD  CONSTRAINT [DF_productProperty_ModifiedAt]  DEFAULT (GETDATE()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[productProperty] ADD  CONSTRAINT [DF_productProperty_Status]  DEFAULT ((10)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'User$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'productProperty'
GO
