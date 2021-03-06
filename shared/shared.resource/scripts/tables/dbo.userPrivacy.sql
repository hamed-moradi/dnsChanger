IF(OBJECT_ID('dbo.[userPrivacy]', 'P') IS NOT NULL)
    DROP TABLE dbo.[userPrivacy];
GO

/****** Object:  Table [dbo].[userPrivacy]    Script Date: 5/2/2019 7:46:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userPrivacy](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UserId] INT NOT NULL,
	[PropTypeId] TINYINT NOT NULL,
	[Value] NVARCHAR(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ModifiedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_UserPrivacy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[userPrivacy] ADD  CONSTRAINT [DF_UserPrivacy_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[userPrivacy] ADD  CONSTRAINT [DF_UserPrivacy_ModifiedAt]  DEFAULT (getdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[userPrivacy] ADD  CONSTRAINT [DF_UserPrivacy_Status]  DEFAULT ((10)) FOR [Status]
GO
ALTER TABLE [dbo].[userPrivacy]  WITH CHECK ADD  CONSTRAINT [FK_UserPrivacy_PrivacyType] FOREIGN KEY([PropTypeId])
REFERENCES [dbo].[propertyType] ([Id])
GO
ALTER TABLE [dbo].[userPrivacy] CHECK CONSTRAINT [FK_UserPrivacy_PrivacyType]
GO
ALTER TABLE [dbo].[userPrivacy]  WITH CHECK ADD  CONSTRAINT [FK_UserPrivacy_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([Id])
GO
ALTER TABLE [dbo].[userPrivacy] CHECK CONSTRAINT [FK_UserPrivacy_User]
GO
