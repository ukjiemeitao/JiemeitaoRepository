USE [Catalog]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SS_Product_SS_Brand]') AND parent_object_id = OBJECT_ID(N'[dbo].[SS_Product]'))
ALTER TABLE [dbo].[SS_Product] DROP CONSTRAINT [FK_SS_Product_SS_Brand]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SS_Product_SS_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[SS_Product]'))
ALTER TABLE [dbo].[SS_Product] DROP CONSTRAINT [FK_SS_Product_SS_Product]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SS_Product_SS_Retailers]') AND parent_object_id = OBJECT_ID(N'[dbo].[SS_Product]'))
ALTER TABLE [dbo].[SS_Product] DROP CONSTRAINT [FK_SS_Product_SS_Retailers]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SS_Product_datetimecreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SS_Product] DROP CONSTRAINT [DF_SS_Product_datetimecreated]
END

GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Product]    Script Date: 01/13/2014 21:31:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SS_Product]') AND type in (N'U'))
DROP TABLE [dbo].[SS_Product]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[SS_Product]    Script Date: 01/13/2014 21:31:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SS_Product](
	[id] [uniqueidentifier] NOT NULL,
	[product_id] [bigint] NOT NULL,
	[name] [nvarchar](200) NULL,
	[currency] [nvarchar](10) NULL,
	[price] [numeric](10, 2) NULL,
	[price_label] [nvarchar](20) NULL,
	[sale_price] [numeric](10, 2) NULL,
	[sale_price_label] [nvarchar](20) NULL,
	[in_stock] [bit] NULL,
	[retailer_id] [bigint] NULL,
	[locale] [nvarchar](50) NULL,
	[description] [nvarchar](1000) NULL,
	[brand_id] [bigint] NULL,
	[click_url] [nvarchar](300) NULL,
	[page_url] [nvarchar](300) NULL,
	[image_id] [nvarchar](50) NULL,
	[chinese_name] [nvarchar](200) NULL,
	[chinese_description] [nvarchar](1000) NULL,
	[istranslated] [bit] NULL,
	[datetimecreated] [datetime] NULL,
	[keyword] [nvarchar](50) NULL,
	[product_set_name] [nvarchar](100) NULL,
 CONSTRAINT [PK_SS_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_SS_Product] UNIQUE NONCLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ_SS_Product_Image] UNIQUE NONCLUSTERED 
(
	[image_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SS_Product]  WITH CHECK ADD  CONSTRAINT [FK_SS_Product_SS_Brand] FOREIGN KEY([brand_id])
REFERENCES [dbo].[SS_Brand] ([brand_id])
GO

ALTER TABLE [dbo].[SS_Product] CHECK CONSTRAINT [FK_SS_Product_SS_Brand]
GO

ALTER TABLE [dbo].[SS_Product]  WITH CHECK ADD  CONSTRAINT [FK_SS_Product_SS_Product] FOREIGN KEY([id])
REFERENCES [dbo].[SS_Product] ([id])
GO

ALTER TABLE [dbo].[SS_Product] CHECK CONSTRAINT [FK_SS_Product_SS_Product]
GO

ALTER TABLE [dbo].[SS_Product]  WITH CHECK ADD  CONSTRAINT [FK_SS_Product_SS_Retailers] FOREIGN KEY([retailer_id])
REFERENCES [dbo].[SS_Retailers] ([retailer_id])
GO

ALTER TABLE [dbo].[SS_Product] CHECK CONSTRAINT [FK_SS_Product_SS_Retailers]
GO

ALTER TABLE [dbo].[SS_Product] ADD  CONSTRAINT [DF_SS_Product_datetimecreated]  DEFAULT (getdate()) FOR [datetimecreated]
GO


