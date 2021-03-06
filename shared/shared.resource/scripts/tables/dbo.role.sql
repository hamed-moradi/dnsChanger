IF(OBJECT_ID('dbo.[role]', 'P') IS NOT NULL)
    DROP TABLE dbo.[role];
GO

/****** Object:  Table [dbo].[role]    Script Date: 5/2/2019 7:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](32) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[role] ADD  CONSTRAINT [DF_role_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[role] ADD  CONSTRAINT [DF_role_Status]  DEFAULT ((2)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'Roles', @value=N'Roles$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'role'
GO
