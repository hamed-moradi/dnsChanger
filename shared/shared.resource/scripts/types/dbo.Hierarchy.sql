IF(OBJECT_ID('dbo.[Hierarchy]', 'P') IS NOT NULL)
    DROP TYPE dbo.[Hierarchy];
GO

/****** Object:  UserDefinedTableType [dbo].[Hierarchy]    Script Date: 5/2/2019 7:46:11 PM ******/
CREATE TYPE [dbo].[Hierarchy] AS TABLE(
	[ElementId] INT NOT NULL,
	[SequenceNo] INT NULL,
	[ParentId] INT NULL,
	[ObjectId] INT NULL,
	[Name] NVARCHAR(MAX) NULL,
	[StringValue] NVARCHAR(MAX) NOT NULL,
	[ValueType] VARCHAR(10) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ElementId] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
