USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Color]    Script Date: 12/30/2013 11:09:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Color]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Color]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Color]    Script Date: 12/30/2013 11:09:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Color](
	[id] [uniqueidentifier] NOT NULL,
	[color_id] [bigint] NULL,
	[color_name] [nvarchar](50) NULL,
	[url] [nvarchar](300) NULL,
 CONSTRAINT [PK_SS_Color] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_SS_Color] UNIQUE NONCLUSTERED 
(
	[color_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

