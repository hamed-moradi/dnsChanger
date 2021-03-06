IF(OBJECT_ID('dbo.[smsStatus]', 'P') IS NOT NULL)
    DROP TABLE dbo.[smsStatus];
GO

/****** Object:  Table [dbo].[smsStatus]    Script Date: 5/2/2019 7:46:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[smsStatus](
	[Id] [tinyint] NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_MustToSendSmsStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
