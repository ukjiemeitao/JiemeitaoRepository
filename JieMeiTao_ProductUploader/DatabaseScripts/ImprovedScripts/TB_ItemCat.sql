USE [Catalog]
GO

/****** Object:  Table [dbo].[TB_ItemCat]    Script Date: 01/05/2014 22:24:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TB_ItemCat]') AND type in (N'U'))
DROP TABLE [dbo].[TB_ItemCat]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[TB_ItemCat]    Script Date: 01/05/2014 22:24:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_ItemCat](
	[ID] [uniqueidentifier] NOT NULL,
	[Cid] [bigint] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[IsParent] [bit] NULL,
	[ModifiedTime] [datetime] NULL,
	[ModifiedType] [nchar](50) NULL,
	[ParentCid] [bigint] NULL,
	[SortOrder] [bigint] NULL,
	[Status] [nchar](50) NULL,
 CONSTRAINT [PK_TB_ItemCat] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_TB_ItemCat] UNIQUE NONCLUSTERED 
(
	[Cid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表主键 自增长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'Cid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类目名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该类目是否为父类目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'IsParent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改类型 三种枚举类型：modify，add，delete' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'ModifiedType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父类目ID (等于0时,代表的是一级的类目)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'ParentCid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排列序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'SortOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态。可选值:normal(正常),deleted(删除)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemCat', @level2type=N'COLUMN',@level2name=N'Status'
GO


