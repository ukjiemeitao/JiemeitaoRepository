USE [Catalog]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SS_Product_Category_Mapping_SS_Category]') AND parent_object_id = OBJECT_ID(N'[dbo].[SS_Product_Category_Mapping]'))
ALTER TABLE [dbo].[SS_Product_Category_Mapping] DROP CONSTRAINT [FK_SS_Product_Category_Mapping_SS_Category]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SS_Product_Category_Mapping_SS_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[SS_Product_Category_Mapping]'))
ALTER TABLE [dbo].[SS_Product_Category_Mapping] DROP CONSTRAINT [FK_SS_Product_Category_Mapping_SS_Product]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Product_Category_Mapping]    Script Date: 12/30/2013 11:10:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Product_Category_Mapping]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Product_Category_Mapping]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Product_Category_Mapping]    Script Date: 12/30/2013 11:10:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Product_Category_Mapping](
	[id] [uniqueidentifier] NOT NULL,
	[product_id] [bigint] NOT NULL,
	[cat_id] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_SS_Product_Category_Mapping] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SS_Product_Category_Mapping]  WITH CHECK ADD  CONSTRAINT [FK_SS_Product_Category_Mapping_SS_Category] FOREIGN KEY([cat_id])
REFERENCES [dbo].[SS_Category] ([cat_id])
GO

ALTER TABLE [dbo].[SS_Product_Category_Mapping] CHECK CONSTRAINT [FK_SS_Product_Category_Mapping_SS_Category]
GO

ALTER TABLE [dbo].[SS_Product_Category_Mapping]  WITH CHECK ADD  CONSTRAINT [FK_SS_Product_Category_Mapping_SS_Product] FOREIGN KEY([product_id])
REFERENCES [dbo].[SS_Product] ([product_id])
GO

ALTER TABLE [dbo].[SS_Product_Category_Mapping] CHECK CONSTRAINT [FK_SS_Product_Category_Mapping_SS_Product]
GO

