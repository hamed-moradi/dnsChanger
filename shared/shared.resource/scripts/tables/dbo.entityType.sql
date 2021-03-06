IF(OBJECT_ID('dbo.[entityType]', 'P') IS NOT NULL)
    DROP TABLE dbo.[entityType];
GO

/****** Object:  Table [dbo].[entityType]    Script Date: 5/2/2019 7:46:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[entityType](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_EntityType] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
