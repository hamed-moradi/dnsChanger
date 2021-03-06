IF(OBJECT_ID('dbo.[business]', 'P') IS NOT NULL)
    DROP TABLE dbo.[business];
GO

/****** Object:  Table [dbo].[business]    Script Date: 5/2/2019 7:46:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[business](
	[Id] INT IDENTITY(1,1) NOT NULL,
	[CustomerId] INT NOT NULL,
	[Title] NVARCHAR(64) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Phone] VARCHAR(64) NULL,
	[Address] NVARCHAR(64) NULL,
	[BusinessCode] VARCHAR(16) NULL,
	[Thumbnail] NVARCHAR(512) NULL,
	[Latitude] FLOAT NOT NULL,
	[Longitude] FLOAT NOT NULL,
	[Point] GEOGRAPHY NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
	[ModifiedAt] DATETIME NOT NULL,
	[Status] TINYINT NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[business] ADD  CONSTRAINT [DF_business_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[business] ADD  CONSTRAINT [DF_business_ModifiedAt]  DEFAULT (GETDATE()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[business] ADD  CONSTRAINT [DF_business_Status]  DEFAULT ((10)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'AppSection', @value=N'User$$$list|add|delete|edit|filter|print|excel|details' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'business'
GO
