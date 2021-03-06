IF(OBJECT_ID('dbo.[gender]', 'P') IS NOT NULL)
    DROP TABLE dbo.[gender];
GO

/****** Object:  Table [dbo].[gender]    Script Date: 5/2/2019 7:46:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gender](
	[Id] [tinyint] NOT NULL,
	[Title] [nvarchar](16) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_gender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
