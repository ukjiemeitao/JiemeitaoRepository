USE [Catalog]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TB_ItemProp_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TB_ItemProp] DROP CONSTRAINT [DF_TB_ItemProp_ID]
END

GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[TB_ItemProp]    Script Date: 12/30/2013 11:12:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TB_ItemProp]') AND type in (N'U'))
DROP TABLE [dbo].[TB_ItemProp]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[TB_ItemProp]    Script Date: 12/30/2013 11:12:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_ItemProp](
	[ID] [uniqueidentifier] NOT NULL,
	[Cid] [bigint] NULL,
	[Pid] [bigint] NULL,
	[ParentPid] [bigint] NULL,
	[ParentVid] [bigint] NULL,
	[ChildTemplate] [nvarchar](2000) NULL,
	[IsAllowAlias] [bit] NULL,
	[IsColorProp] [bit] NULL,
	[IsEnumProp] [bit] NULL,
	[IsInputProp] [bit] NULL,
	[IsItemProp] [bit] NULL,
	[IsKeyProp] [bit] NULL,
	[IsSaleProp] [bit] NULL,
	[ModifiedTime] [datetime] NULL,
	[ModifiedType] [nchar](50) NULL,
	[Multi] [bit] NULL,
	[Must] [bit] NULL,
	[Name] [nvarchar](200) NULL,
	[Required] [bit] NULL,
	[SortOrder] [bigint] NULL,
	[Status] [nchar](50) NULL,
 CONSTRAINT [PK_TB_ItemProp] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表主键，自增长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'Cid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'Pid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级属性ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'ParentPid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级属性值ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'ParentVid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子属性的模板' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'ChildTemplate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否允许别名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'IsAllowAlias'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否颜色属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'IsColorProp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是可枚举属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'IsEnumProp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在IsEnumProp是true的前提下，是否是卖家可以自行输入的属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'IsInputProp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否商品属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'IsItemProp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否关键属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'IsKeyProp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否销售属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'IsSaleProp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改类型  三种枚举类型：modify，add，delete' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'ModifiedType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布产品或商品时是否可以多选' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'Multi'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布产品或商品时是否为必选属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'Must'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布产品或商品时是否为必选属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'Required'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排列序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'SortOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态。可选值:normal(正常),deleted(删除)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_ItemProp', @level2type=N'COLUMN',@level2name=N'Status'
GO

ALTER TABLE [dbo].[TB_ItemProp] ADD  CONSTRAINT [DF_TB_ItemProp_ID]  DEFAULT (newid()) FOR [ID]
GO

