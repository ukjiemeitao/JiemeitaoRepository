USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Retailers]    Script Date: 12/30/2013 11:11:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Retailers]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Retailers]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Retailers]    Script Date: 12/30/2013 11:11:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Retailers](
	[id] [uniqueidentifier] NOT NULL,
	[retailer_id] [bigint] NOT NULL,
	[name] [nvarchar](80) NULL,
	[url] [nvarchar](300) NULL,
	[deeplinkSupport] [bit] NULL,
 CONSTRAINT [PK_SS_Retailers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_SS_Retailers] UNIQUE NONCLUSTERED 
(
	[retailer_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

