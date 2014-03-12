USE [Catalog]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Mapping_Categories_TB_ItemCat]') AND parent_object_id = OBJECT_ID(N'[dbo].[Mapping_Categories]'))
ALTER TABLE [dbo].[Mapping_Categories] DROP CONSTRAINT [FK_Mapping_Categories_TB_ItemCat]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[Mapping_Categories]    Script Date: 01/07/2014 18:29:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mapping_Categories]') AND type in (N'U'))
DROP TABLE [dbo].[Mapping_Categories]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[Mapping_Categories]    Script Date: 01/07/2014 18:29:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Mapping_Categories](
	[tb_cid] [bigint] NOT NULL,
	[ss_cid_array] [nvarchar](100) NULL,
	[keywords] [nvarchar](50) NULL,
	[keywords_category] [nvarchar](100) NULL,
 CONSTRAINT [PK_Mapping_Categories] PRIMARY KEY CLUSTERED 
(
	[tb_cid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Mapping_Categories] UNIQUE NONCLUSTERED 
(
	[tb_cid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'多个分类id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mapping_Categories', @level2type=N'COLUMN',@level2name=N'ss_cid_array'
GO

ALTER TABLE [dbo].[Mapping_Categories]  WITH CHECK ADD  CONSTRAINT [FK_Mapping_Categories_TB_ItemCat] FOREIGN KEY([tb_cid])
REFERENCES [dbo].[TB_ItemCat] ([Cid])
GO

ALTER TABLE [dbo].[Mapping_Categories] CHECK CONSTRAINT [FK_Mapping_Categories_TB_ItemCat]
GO


