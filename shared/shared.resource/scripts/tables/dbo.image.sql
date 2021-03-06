IF(OBJECT_ID('dbo.[image]', 'P') IS NOT NULL)
    DROP TABLE dbo.[image];
GO

/****** Object:  Table [dbo].[image]    Script Date: 5/2/2019 7:46:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[image](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[EntityId] INT NOT NULL,
	[ScaleId] INT NOT NULL,
	[Entity] VARCHAR(32) NOT NULL,
	[Name] NVARCHAR(128) NOT NULL,
	[Extension] NVARCHAR(8) NOT NULL,
	[Path] NVARCHAR(512) NULL,
	[Description] NVARCHAR(MAX) NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ModifiedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[image] ADD  CONSTRAINT [DF_Image_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[image] ADD  CONSTRAINT [DF_Image_ModifiedAt]  DEFAULT (GETDATE()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[image] ADD  CONSTRAINT [DF_Image_Status]  DEFAULT ((10)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'User$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image'
GO
