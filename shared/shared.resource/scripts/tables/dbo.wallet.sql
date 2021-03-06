IF(OBJECT_ID('dbo.[wallet]', 'P') IS NOT NULL)
    DROP TABLE dbo.[wallet];
GO

/****** Object:  Table [dbo].[wallet]    Script Date: 5/2/2019 7:46:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wallet](
	[UserId] [int] NOT NULL,
	[Cash] [int] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[UpdatedAt] [smalldatetime] NULL,
	[CreatedAt] [smalldatetime] NOT NULL,
	[UpdaterId] [int] NULL,
 CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'Wallet$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wallet'
GO
