IF(OBJECT_ID('dbo.[ArrayList]', 'P') IS NOT NULL)
    DROP TYPE dbo.[ArrayList];
GO

/****** Object:  UserDefinedTableType [dbo].[ArrayList]    Script Date: 5/2/2019 7:46:10 PM ******/
CREATE TYPE [dbo].[ArrayList] AS TABLE(
	[Id] INT NULL
)
GO
