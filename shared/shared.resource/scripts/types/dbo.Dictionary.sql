IF(OBJECT_ID('dbo.[Dictionary]', 'P') IS NOT NULL)
    DROP TYPE dbo.[Dictionary];
GO

/****** Object:  UserDefinedTableType [dbo].[Dictionary]    Script Date: 5/2/2019 7:46:11 PM ******/
CREATE TYPE [dbo].[Dictionary] AS TABLE(
	[Key] NVARCHAR(MAX) NOT NULL,
	[Value] NVARCHAR(MAX) NOT NULL
)
GO
