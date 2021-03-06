IF(OBJECT_ID('dbo.[user]', 'P') IS NOT NULL)
    DROP TABLE dbo.[user];
GO

/****** Object:  Table [dbo].[user]    Script Date: 5/2/2019 7:46:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Family] [nvarchar](64) NOT NULL,
	[Nickname] [nvarchar](64) NULL,
	[Username] [varchar](16) NOT NULL,
	[Password] [varchar](32) NULL,
	[Avatar] [nvarchar](512) NULL,
	[ModifiedAt] [datetime] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Developer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF__user__ModifiedAt__69279377]  DEFAULT (getdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_Status]  DEFAULT ((10)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'Developer$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user'
GO
