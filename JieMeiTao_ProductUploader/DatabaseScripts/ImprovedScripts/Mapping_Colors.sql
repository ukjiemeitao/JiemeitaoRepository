USE [Catalog]
GO

/****** Object:  Table [dbo].[Mapping_Colors]    Script Date: 01/22/2014 13:20:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mapping_Colors]') AND type in (N'U'))
DROP TABLE [dbo].[Mapping_Colors]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[Mapping_Colors]    Script Date: 01/22/2014 13:20:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Mapping_Colors](
	[id] [uniqueidentifier] NOT NULL,
	[ss_color] [nvarchar](50) NULL,
	[tb_color] [nvarchar](50) NULL,
	[tb_vid] [bigint] NULL,
 CONSTRAINT [PK_Mapping_Colors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


