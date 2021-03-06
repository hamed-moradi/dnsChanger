IF(OBJECT_ID('dbo.[api_customer_getById]', 'P') IS NOT NULL)
    DROP PROCEDURE dbo.[api_customer_getById];
GO

/****** Object:  StoredProcedure [dbo].[api_customer_getById]    Script Date: 5/2/2019 7:46:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[api_customer_getById]
	@Token VARCHAR(32),
	@DeviceId VARCHAR(128),
	@Id INT
AS
BEGIN
  SET NOCOUNT ON;

  SELECT * FROM dbo.customer WITH(NOLOCK) WHERE AdminId = @Id
END
GO
