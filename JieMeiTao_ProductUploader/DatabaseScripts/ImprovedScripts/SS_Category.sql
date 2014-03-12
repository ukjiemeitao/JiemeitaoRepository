USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Category]    Script Date: 12/30/2013 11:09:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Category]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Category]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Category]    Script Date: 12/30/2013 11:09:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Category](
	[id] [uniqueidentifier] NOT NULL,
	[cat_id] [nvarchar](100) NOT NULL,
	[localizedid] [nvarchar](100) NULL,
	[shortname] [nvarchar](80) NULL,
	[name] [nvarchar](50) NULL,
	[parentid] [nvarchar](100) NULL,
 CONSTRAINT [PK_SS_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_SS_Category] UNIQUE NONCLUSTERED 
(
	[cat_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

