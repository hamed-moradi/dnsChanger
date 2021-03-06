IF(OBJECT_ID('dbo.[messageType]', 'P') IS NOT NULL)
    DROP TABLE dbo.[messageType];
GO

/****** Object:  Table [dbo].[messageType]    Script Date: 5/2/2019 7:46:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[messageType](
	[Id] TINYINT IDENTITY(1,1) NOT NULL,
	[Title] VARCHAR(32) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
 CONSTRAINT [PK_MessageType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نوع پیام ها' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'messageType'
GO
