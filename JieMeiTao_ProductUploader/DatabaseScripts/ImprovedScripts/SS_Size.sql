USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Size]    Script Date: 12/30/2013 11:11:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Size]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Size]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Size]    Script Date: 12/30/2013 11:11:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Size](
	[id] [uniqueidentifier] NOT NULL,
	[size_id] [nvarchar](5) NULL,
	[name] [nvarchar](20) NULL,
	[cat_id] [nvarchar](100) NULL,
 CONSTRAINT [PK_SS_Size] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

