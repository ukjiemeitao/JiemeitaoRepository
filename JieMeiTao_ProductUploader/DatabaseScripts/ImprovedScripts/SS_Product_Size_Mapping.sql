USE [Catalog]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SS_Product_Size_Mapping_SS_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[SS_Product_Size_Mapping]'))
ALTER TABLE [dbo].[SS_Product_Size_Mapping] DROP CONSTRAINT [FK_SS_Product_Size_Mapping_SS_Product]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SS_Product_Size_Mapping_SS_Size]') AND parent_object_id = OBJECT_ID(N'[dbo].[SS_Product_Size_Mapping]'))
ALTER TABLE [dbo].[SS_Product_Size_Mapping] DROP CONSTRAINT [FK_SS_Product_Size_Mapping_SS_Size]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Product_Size_Mapping]    Script Date: 12/30/2013 11:10:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Product_Size_Mapping]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Product_Size_Mapping]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Product_Size_Mapping]    Script Date: 12/30/2013 11:10:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Product_Size_Mapping](
	[id] [uniqueidentifier] NOT NULL,
	[product_id] [bigint] NOT NULL,
	[size_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_SS_Product_Size_Mapping] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SS_Product_Size_Mapping]  WITH CHECK ADD  CONSTRAINT [FK_SS_Product_Size_Mapping_SS_Product] FOREIGN KEY([product_id])
REFERENCES [dbo].[SS_Product] ([product_id])
GO

ALTER TABLE [dbo].[SS_Product_Size_Mapping] CHECK CONSTRAINT [FK_SS_Product_Size_Mapping_SS_Product]
GO

ALTER TABLE [dbo].[SS_Product_Size_Mapping]  WITH CHECK ADD  CONSTRAINT [FK_SS_Product_Size_Mapping_SS_Size] FOREIGN KEY([size_id])
REFERENCES [dbo].[SS_Size] ([id])
GO

ALTER TABLE [dbo].[SS_Product_Size_Mapping] CHECK CONSTRAINT [FK_SS_Product_Size_Mapping_SS_Size]
GO

