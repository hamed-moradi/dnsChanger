IF(OBJECT_ID('dbo.[propertyType]', 'P') IS NOT NULL)
    DROP TABLE dbo.[propertyType];
GO

/****** Object:  Table [dbo].[propertyType]    Script Date: 5/2/2019 7:46:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[propertyType](
	[Id] [tinyint] NOT NULL,
	[Title] [nvarchar](16) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_PropertyType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
