IF(OBJECT_ID('dbo.[session]', 'P') IS NOT NULL)
    DROP TABLE dbo.[session];
GO

/****** Object:  Table [dbo].[session]    Script Date: 5/2/2019 7:46:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[session](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UserId] INT NOT NULL,
	[Token] CHAR(32) NOT NULL,
	[DeviceId] VARCHAR(128) NOT NULL,
	[IP] VARCHAR(16) NOT NULL,
	[OS] VARCHAR(128) NULL,
	[Version] VARCHAR(128) NULL,
	[DeviceName] VARCHAR(128) NULL,
	[Browser] VARCHAR(128) NULL,
	[TimeZone] VARCHAR(16) NULL,
	[Language] VARCHAR(8) NULL,
	[FCM] VARCHAR(MAX) NULL,
	[ModifiedAt] DATETIME NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[session] ADD  CONSTRAINT [DF_session_ModifiedAt]  DEFAULT (getdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[session]  WITH CHECK ADD  CONSTRAINT [FK_Session_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([Id])
GO
ALTER TABLE [dbo].[session] CHECK CONSTRAINT [FK_Session_User]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'Session$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'session'
GO