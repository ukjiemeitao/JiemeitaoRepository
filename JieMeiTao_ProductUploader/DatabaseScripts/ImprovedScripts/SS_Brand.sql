USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Brand]    Script Date: 12/30/2013 11:08:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Brand]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Brand]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Brand]    Script Date: 12/30/2013 11:08:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Brand](
	[id] [uniqueidentifier] NOT NULL,
	[brand_id] [bigint] NOT NULL,
	[brand_name] [nvarchar](100) NULL,
	[url] [nvarchar](300) NULL,
 CONSTRAINT [PK_SS_Brand] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_SS_Brand] UNIQUE NONCLUSTERED 
(
	[brand_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

