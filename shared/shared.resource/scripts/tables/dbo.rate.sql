IF(OBJECT_ID('dbo.[rate]', 'P') IS NOT NULL)
    DROP TABLE dbo.[rate];
GO

/****** Object:  Table [dbo].[rate]    Script Date: 5/2/2019 7:46:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Point] [decimal](2, 1) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[ReplyTo] [int] NULL,
	[UserId] [int] NULL,
	[AdminId] [int] NULL,
	[GameId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdaterId] [int] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Rate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Rate] UNIQUE NONCLUSTERED 
(
	[GameId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[rate]  WITH NOCHECK ADD  CONSTRAINT [FK_Rate_Rate] FOREIGN KEY([ReplyTo])
REFERENCES [dbo].[rate] ([Id])
GO
ALTER TABLE [dbo].[rate] NOCHECK CONSTRAINT [FK_Rate_Rate]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'Rate$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rate'
GO
