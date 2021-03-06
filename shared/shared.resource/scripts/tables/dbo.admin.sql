IF(OBJECT_ID('dbo.[admin]', 'P') IS NOT NULL)
    DROP TABLE dbo.[admin];
GO

/****** Object:  Table [dbo].[admin]    Script Date: 5/2/2019 7:46:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[RoleId] INT NOT NULL,
	[Username] NVARCHAR(32) NOT NULL,
	[Password] NVARCHAR(32) NOT NULL,
	[Name] NVARCHAR(32) NULL,
	[Family] NVARCHAR(32) NULL,
	[Cellphone] NVARCHAR(16) NOT NULL,
	[Email] VARCHAR(64) NULL,
	[Gender] TINYINT NULL,
	[Avatar] NVARCHAR(512) NULL,
	[LastSignedin] DATETIME NULL,
	[CreatedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [IX_Admin] UNIQUE NONCLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_admin_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_admin_Status]  DEFAULT ((2)) FOR [Status]
GO
ALTER TABLE [dbo].[admin]  WITH CHECK ADD  CONSTRAINT [FK_admin_gender] FOREIGN KEY([Gender])
REFERENCES [dbo].[gender] ([Id])
GO
ALTER TABLE [dbo].[admin] CHECK CONSTRAINT [FK_admin_gender]
GO
ALTER TABLE [dbo].[admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[role] ([Id])
GO
ALTER TABLE [dbo].[admin] CHECK CONSTRAINT [FK_Admin_Role]
GO
