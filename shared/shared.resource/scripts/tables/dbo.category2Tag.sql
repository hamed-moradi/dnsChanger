IF(OBJECT_ID('dbo.[category2Tag]', 'P') IS NOT NULL)
    DROP TABLE dbo.[category2Tag];
GO

/****** Object:  Table [dbo].[category2Tag]    Script Date: 5/2/2019 7:46:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category2Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_Category2Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[category2Tag]  WITH CHECK ADD  CONSTRAINT [FK_Category2Tag_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[category] ([Id])
GO
ALTER TABLE [dbo].[category2Tag] CHECK CONSTRAINT [FK_Category2Tag_Category]
GO
ALTER TABLE [dbo].[category2Tag]  WITH CHECK ADD  CONSTRAINT [FK_Category2Tag_Tag] FOREIGN KEY([TagId])
REFERENCES [dbo].[tag] ([Id])
GO
ALTER TABLE [dbo].[category2Tag] CHECK CONSTRAINT [FK_Category2Tag_Tag]
GO
