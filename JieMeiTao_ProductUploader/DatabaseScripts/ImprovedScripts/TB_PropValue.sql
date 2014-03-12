USE [Catalog]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_TB_PropValue_ID]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TB_PropValue] DROP CONSTRAINT [DF_TB_PropValue_ID]
END

GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[TB_PropValue]    Script Date: 12/30/2013 11:12:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TB_PropValue]') AND type in (N'U'))
DROP TABLE [dbo].[TB_PropValue]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[TB_PropValue]    Script Date: 12/30/2013 11:12:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TB_PropValue](
	[ID] [uniqueidentifier] NOT NULL,
	[Cid] [bigint] NULL,
	[IsParent] [bit] NULL,
	[ModifiedTime] [datetime] NULL,
	[ModifiedType] [nchar](50) NULL,
	[Name] [nvarchar](200) NULL,
	[NameAlias] [nvarchar](200) NULL,
	[Pid] [bigint] NULL,
	[PropName] [nvarchar](200) NULL,
	[SortOrder] [bigint] NULL,
	[Status] [nchar](50) NULL,
	[Vid] [bigint] NULL,
 CONSTRAINT [PK_TB_PropValue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表主键 自增长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'Cid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为父类目属性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'IsParent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'ModifiedTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改类型 三种枚举类型：modify，add，delete ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'ModifiedType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性值别名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'NameAlias'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'Pid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'PropName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排列序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'SortOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态。可选值:normal(正常),deleted(删除)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性值ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_PropValue', @level2type=N'COLUMN',@level2name=N'Vid'
GO

ALTER TABLE [dbo].[TB_PropValue] ADD  CONSTRAINT [DF_TB_PropValue_ID]  DEFAULT (newid()) FOR [ID]
GO

