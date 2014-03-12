USE [Catalog]
GO

/****** Object:  Table [dbo].[Mapping_Sizes]    Script Date: 01/22/2014 16:04:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mapping_Sizes]') AND type in (N'U'))
DROP TABLE [dbo].[Mapping_Sizes]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[Mapping_Sizes]    Script Date: 01/22/2014 16:04:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Mapping_Sizes](
	[id] [uniqueidentifier] NOT NULL,
	[ss_size_name] [nvarchar](50) NULL,
	[tb_size_name] [nvarchar](50) NULL,
	[tb_vid] [bigint] NULL
) ON [PRIMARY]

GO


