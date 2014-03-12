USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Images]    Script Date: 12/30/2013 11:09:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Images]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Images]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Images]    Script Date: 12/30/2013 11:09:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Images](
	[id] [uniqueidentifier] NOT NULL,
	[image_id] [nvarchar](50) NULL,
	[size_name] [nvarchar](30) NULL,
	[width] [int] NULL,
	[height] [int] NULL,
	[url] [nvarchar](300) NULL,
 CONSTRAINT [PK_SS_Images] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

