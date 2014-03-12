USE [Catalog]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TB_Product_SS_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[TB_Product]'))
ALTER TABLE [dbo].[TB_Product] DROP CONSTRAINT [FK_TB_Product_SS_Product]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[TB_Product]    Script Date: 01/15/2014 17:06:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TB_Product]') AND type in (N'U'))
DROP TABLE [dbo].[TB_Product]
GO

USE [Catalog]
GO

/****** Object:  Table [dbo].[TB_Product]    Script Date: 01/15/2014 17:06:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TB_Product](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](30) NULL,
	[Desc] [text] NULL,
	[Price] [decimal](18, 2) NULL,
	[StuffStatus] [varchar](50) NULL,
	[Cid] [bigint] NULL,
	[PropertyAlias] [nvarchar](511) NULL,
	[Props] [nvarchar](549) NULL,
	[PicPath] [nchar](1000) NULL,
	[Num] [bigint] NULL,
	[Type] [varchar](50) NULL,
	[LocationState] [nvarchar](100) NULL,
	[LocationCity] [nvarchar](100) NULL,
	[ApproveStatus] [varchar](50) NULL,
	[FreightPayer] [varchar](50) NULL,
	[ValidThru] [bigint] NULL,
	[HasInvoice] [bit] NULL,
	[HasWarranty] [bit] NULL,
	[HasShowcase] [bit] NULL,
	[SellerCids] [nvarchar](200) NULL,
	[InputPids] [nvarchar](200) NULL,
	[InputStr] [nvarchar](200) NULL,
	[HasDiscount] [bit] NULL,
	[PostFee] [decimal](9, 2) NULL,
	[ExpressFee] [decimal](9, 2) NULL,
	[EmsFee] [decimal](9, 2) NULL,
	[ListTime] [datetime] NULL,
	[Increment] [nvarchar](50) NULL,
	[ImgFilePath] [nvarchar](256) NULL,
	[PostageId] [bigint] NULL,
	[AuctionPoint] [bigint] NULL,
	[SkuProperties] [nvarchar](4000) NULL,
	[SkuQuantities] [nvarchar](4000) NULL,
	[SkuPrices] [nvarchar](4000) NULL,
	[SkuOuterIds] [nvarchar](4000) NULL,
	[Lang] [nvarchar](50) NULL,
	[OuterId] [nvarchar](512) NULL,
	[IsTaobao] [bit] NULL,
	[IsEx] [bit] NULL,
	[Is3D] [bit] NULL,
	[SellPromise] [bit] NULL,
	[AfterSaleId] [bigint] NULL,
	[CodPostageId] [bigint] NULL,
	[IsLightningConsignment] [bit] NULL,
	[Weight] [bigint] NULL,
	[IsXinpin] [bit] NULL,
	[SubStock] [bigint] NULL,
	[ItemSize] [decimal](9, 2) NULL,
	[ItemWeight] [decimal](9, 2) NULL,
	[ChangeProp] [nvarchar](2000) NULL,
	[DescModules] [nvarchar](2000) NULL,
	[GlobalStockType] [nvarchar](50) NULL,
	[GlobalStockCountry] [nvarchar](200) NULL,
	[NumIid] [bigint] NULL,
	[IsReplaceSku] [bit] NULL,
	[EmptyFields] [nvarchar](4000) NULL,
	[Status] [int] NULL,
	[SSProductID] [bigint] NULL,
	[IsUpdated] [bit] NULL,
 CONSTRAINT [PK_TB_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表主键 GUID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宝贝标题。不能超过30字符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Title'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宝贝描述。字数要大于5个字符，小于25000个字符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Desc'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Price'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宝贝类型：可选值：new(新)，second(二手)，unused(闲置)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'StuffStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类目ID.必须是叶子类目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Cid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性值别名。如pid:vid:别名;pid1:vid1:别名1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'PropertyAlias'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' 商品属性列表。格式:pid:vid;pid:vid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Props'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品主图需要关联的图片空间的相对url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'PicPath'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量，取值范围:0-900000000的整数。且需要等于Sku所有数量的和。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Num'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布类型。可选值:fixed(一口价),auction(拍卖)。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所在地省份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'LocationState'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所在地城市' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'LocationCity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品上传后的状态。可选值:onsale(出售中),instock(仓库中);默认值:onsale ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ApproveStatus'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费承担方式。可选值:seller（卖家承担）,buyer(买家承担);默认值:seller。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'FreightPayer'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有效期。可选值:7,14;单位:天;默认值:14 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ValidThru'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有发票。可选值:true,false (商城卖家此字段必须为true);默认值:false(无发票)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'HasInvoice'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有保修。可选值:true,false;默认值:false(不保修)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'HasWarranty'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'橱窗推荐。可选值:true,false;默认值:false(不推荐) ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'HasShowcase'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品所属的店铺类目列表。按逗号分隔。结构:",cid1,cid2,...,"' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'SellerCids'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户自行输入的类目属性ID串。结构："pid1,pid2,pid3"，如："20000"（表示品牌）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'InputPids'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户自行输入的子属性名和属性值，结构:"父属性值;一级子属性名;一级子属性值;二级子属性名;自定义输入值,...."' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'InputStr'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支持会员打折。可选值:true,false;默认值:false(不打折)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'HasDiscount'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平邮费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'PostFee'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快递费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ExpressFee'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ems费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'EmsFee'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'定时上架时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ListTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加价(降价)幅度。如果为0，代表系统代理幅度。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Increment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品主图片路径 支持的文件类型：gif,jpg,jpeg,png ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ImgFilePath'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宝贝所属的运费模板ID。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'PostageId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' 商品的积分返点比例。如:5,表示:返点比例0.5%' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'AuctionPoint'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sku的属性串 格式:pid:vid;pid:vid,多个sku之间用逗号分隔。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'SkuProperties'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sku的数量串，结构如：num1,num2,num3 如：2,3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'SkuQuantities'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sku的价格串，结构如：10.00,5.00,… 精确到2位小数;单位:元。如:200.07，表示:200元7分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'SkuPrices'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sku的外部id串，结构如：1234,1342,… sku_properties, sku_quantities, sku_prices, sku_outer_ids在输入数据时要一一对应，如果没有sku_outer_ids也要写上这个参数，入参是","(这个是两个sku的示列，逗号数应该是sku个数减1)；' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'SkuOuterIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品文字的字符集。繁体传入"zh_HK"，简体传入"zh_CN"，不传默认为简体 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Lang'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品外部编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'OuterId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否在淘宝上显示（如果传FALSE，则在淘宝主站无法显示该商品） ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'IsTaobao'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否在外店显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'IsEx'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是3D' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Is3D'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否承诺退换货服务' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'SellPromise'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售后说明模板id ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'AfterSaleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'到付款运费模板的ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'CodPostageId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实物闪电发货' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'IsLightningConsignment'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品的重量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Weight'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品是否为新品。只有在当前类目开通新品,并且当前用户拥有该类目下发布新品权限时才能设置is_xinpin为true' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'IsXinpin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品是否支持拍下减库存:1支持;2取消支持(付款减库存);0(默认)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'SubStock'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品的体积，用于按体积计费的运费模板。注意：单位为立方米。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ItemSize'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品的重量，用于按重量计费的运费模板。注意：单位为kg。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ItemWeight'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品基础色，数据格式为：pid:vid:rvid1,rvid2,rvid3;pid:vid:rvid1; ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'ChangeProp'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品描述模块化，模块列表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'DescModules'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全球购商品采购地（库存类型）， 有两种库存类型：现货和代购 参数值为1时代表现货，值为2时代表代购。注意：使用时请与 全球购商品采购地（地区/国家）配合使用 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'GlobalStockType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全球购商品采购地（地区/国家）,默认值只在全球购商品采购地' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'GlobalStockCountry'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数字ID，该参数必须(更新商品信息时必填)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'NumIid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否替换sku ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'IsReplaceSku'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支持对全球购宝贝信息的清除（字符串中包含global_stock） ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'EmptyFields'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态  1：已入库 2：已上传' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TB_Product', @level2type=N'COLUMN',@level2name=N'Status'
GO

ALTER TABLE [dbo].[TB_Product]  WITH CHECK ADD  CONSTRAINT [FK_TB_Product_SS_Product] FOREIGN KEY([SSProductID])
REFERENCES [dbo].[SS_Product] ([product_id])
GO

ALTER TABLE [dbo].[TB_Product] CHECK CONSTRAINT [FK_TB_Product_SS_Product]
GO


