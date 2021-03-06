IF(OBJECT_ID('dbo.[productPropertyType]', 'P') IS NOT NULL)
    DROP TABLE dbo.[productPropertyType];
GO

/****** Object:  Table [dbo].[productPropertyType]    Script Date: 5/2/2019 7:46:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productPropertyType](
	[Id] SMALLINT IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(512) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[CreatedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[productPropertyType] ADD  CONSTRAINT [DF_productPropertyType_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[productPropertyType] ADD  CONSTRAINT [DF_productPropertyType_Status]  DEFAULT ((10)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'User$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'productPropertyType'
GO
