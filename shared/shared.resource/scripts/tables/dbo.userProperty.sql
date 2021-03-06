IF(OBJECT_ID('dbo.[userProperty]', 'P') IS NOT NULL)
    DROP TABLE dbo.[userProperty];
GO

/****** Object:  Table [dbo].[userProperty]    Script Date: 5/2/2019 7:46:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userProperty](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UserId] INT NOT NULL,
	[PropTypeId] TINYINT NOT NULL,
	[Value] NVARCHAR(MAX) NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ModifiedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_UserProperty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[userProperty] ADD  CONSTRAINT [DF_UserProperty_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[userProperty] ADD  CONSTRAINT [DF_UserProperty_ModifiedAt]  DEFAULT (getdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[userProperty] ADD  CONSTRAINT [DF_UserProperty_Status]  DEFAULT ((10)) FOR [Status]
GO
ALTER TABLE [dbo].[userProperty]  WITH CHECK ADD  CONSTRAINT [FK_UserProperty_PropertyType] FOREIGN KEY([PropTypeId])
REFERENCES [dbo].[propertyType] ([Id])
GO
ALTER TABLE [dbo].[userProperty] CHECK CONSTRAINT [FK_UserProperty_PropertyType]
GO
ALTER TABLE [dbo].[userProperty]  WITH CHECK ADD  CONSTRAINT [FK_UserProperty_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([Id])
GO
ALTER TABLE [dbo].[userProperty] CHECK CONSTRAINT [FK_UserProperty_User]
GO
