USE [Catalog]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SS_Brand_Synonyms_SS_Brand]') AND parent_object_id = OBJECT_ID(N'[dbo].[SS_Brand_Synonyms]'))
ALTER TABLE [dbo].[SS_Brand_Synonyms] DROP CONSTRAINT [FK_SS_Brand_Synonyms_SS_Brand]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Brand_Synonyms]    Script Date: 12/30/2013 11:08:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Brand_Synonyms]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Brand_Synonyms]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Brand_Synonyms]    Script Date: 12/30/2013 11:08:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Brand_Synonyms](
	[id] [uniqueidentifier] NOT NULL,
	[brand_id] [bigint] NOT NULL,
	[synonyms_name] [nvarchar](100) NULL,
 CONSTRAINT [PK_SS_Brand_Synonyms] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SS_Brand_Synonyms]  WITH CHECK ADD  CONSTRAINT [FK_SS_Brand_Synonyms_SS_Brand] FOREIGN KEY([brand_id])
REFERENCES [dbo].[SS_Brand] ([brand_id])
GO

ALTER TABLE [dbo].[SS_Brand_Synonyms] CHECK CONSTRAINT [FK_SS_Brand_Synonyms_SS_Brand]
GO

