IF(OBJECT_ID('dbo.[historyActionType]', 'P') IS NOT NULL)
    DROP TABLE dbo.[historyActionType];
GO

/****** Object:  Table [dbo].[historyActionType]    Script Date: 5/2/2019 7:46:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[historyActionType](
	[Id] TINYINT IDENTITY(1,1) NOT NULL,
	[Title] NVARCHAR(64) NOT NULL,
	[CreatedAt] DATETIME NOT NULL,
 CONSTRAINT [PK_HistoryAction] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[historyActionType] ADD  CONSTRAINT [DF_HistoryActionType_CreatedAt]  DEFAULT (GETDATE()) FOR [CreatedAt]
GO
