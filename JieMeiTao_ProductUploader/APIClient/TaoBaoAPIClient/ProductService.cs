using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Domain;
using Top.Api.Request;
using Top.Api.Response;
using Top.Api.Util;


namespace TaoBaoAPIClient
{
    /// <summary>
    /// 淘宝API产品相关Service
    /// </summary>
    public static class ProductService
    {
        public static string SessionKey = "6100b1799ac2bcd965cfe0dae1e98cec62519560efd9f9866166345";

        #region 商品上传需要用到的方法
        /// <summary>
        /// taobao.itemcats.authorize.get
        /// 查询B商家被授权品牌列表和类目列表（B商家专用）
        /// </summary>
        /// <param name="fields">必填 需要返回的字段。目前支持有： brand.vid, brand.name, item_cat.cid, item_cat.name, item_cat.status,
        /// item_cat.sort_order,item_cat.parent_cid,item_cat.is_parent, xinpin_item_cat.cid, xinpin_item_cat.name, xinpin_item_cat.status, 
        /// xinpin_item_cat.sort_order, xinpin_item_cat.parent_cid, xinpin_item_cat.is_parent
        /// <returns></returns>
        public static SellerAuthorize GetAuthorizeItemcats(string fields)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemcatsAuthorizeGetRequest req = new ItemcatsAuthorizeGetRequest();
            req.Fields = fields;
            ItemcatsAuthorizeGetResponse response = client.Execute(req, SessionKey);
            return response.SellerAuthorize;
        }

        /// <summary>
        /// taobao.itemcats.get
        /// 获取后台供卖家发布商品的标准商品类目
        /// </summary>
        /// <param name="fields">需要返回的字段列表，ItemCat中所有字段</param>
        /// <param name="parentcid">父商品类目 id，0表示根节点, 传输该参数返回所有子类目。 (cids、parent_cid至少传一个) </param>
        /// <param name="cids">商品所属类目ID列表，用半角逗号(,)分隔 例如:(18957,19562,) (cids、parent_cid至少传一个) </param>
        /// <returns></returns>
        public static List<ItemCat> GetItemcats(string fields, long? parentcid, string cids)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemcatsGetRequest req = new ItemcatsGetRequest();
            req.Fields = fields;
            req.ParentCid = parentcid;
            req.Cids = cids;
            ItemcatsGetResponse response = client.Execute(req);
            return response.ItemCats;
        }

        /// <summary>
        /// taobao.products.search
        /// 搜索产品信息（B商家专用）
        /// </summary>
        /// <param name="fields">必填 需返回的字段列表.可选值:Product数据结构中的以下字段:product_id,name,pic_url,cid,props,price,tsc;多个字段之间用"</param>
        /// <param name="keywords">搜索的关键词是用来搜索产品的title.　注:q,cid和props至少传入一个</param>
        /// <param name="cid">商品类目ID</param>
        /// <param name="props">属性,属性值的组合.格式:pid:vid;pid:vid;</param>
        /// <param name="status">想要获取的产品的状态列表，支持多个状态并列获取，多个状态之间用","分隔，最多同时指定5种状态。例如，只获取小二确认的spu传入"3",只要商家确认的传入"0"，既要小二确认又要商家确认的传入"0,3"。目前只支持者两种类型的状态搜索，输入其他状态无效。</param>
        /// <param name="pageno">页码</param>
        /// <param name="pagesize">每页条数.每页返回最多返回100条,默认值为40. </param>
        /// <param name="verticalmarket">传入值为：3表示3C表示3C垂直市场产品，4表示鞋城垂直市场产品，8表示网游垂直市场产品。一次只能指定一种垂直市场类型 </param>
        /// <param name="customerprops">用户自定义关键属性,结构：pid1:value1;pid2:value2，如果有型号，系列等子属性用: 隔开</param>
        /// <param name="marketid">市场ID，1为取C2C市场的产品信息， 2为取B2C市场的产品信息。 不填写此值则默认取C2C的产品信息。</param>
        /// <returns></returns>
        public static List<Product> GetProductsBySearch(string fields, string keywords, long? cid, string props, string status,
            long? pageno, long? pagesize, long? verticalmarket, string customerprops, string marketid, out long recordcount)
        {
            ITopClient client = TopClientService.GetTopClient();
            ProductsSearchRequest req = new ProductsSearchRequest();
            req.Fields = fields;
            req.Q = keywords;
            req.Cid = cid;
            req.Props = props;
            req.Status = status;
            req.PageNo = pageno;
            req.PageSize = pagesize;
            req.VerticalMarket = verticalmarket;
            req.CustomerProps = customerprops;
            req.MarketId = marketid;
            ProductsSearchResponse response = client.Execute(req);
            recordcount = response.TotalResults;
            return response.Products;
        }

        /// <summary>
        /// taobao.product.add
        /// 上传一个产品，不包括产品非主图和属性图片（B商家专用）
        /// </summary>
        /// <param name="cid">必填 商品类目ID.调用taobao.itemcats.get获取;注意:必须是叶子类目 id. </param>
        /// <param name="imagefilepath">必填 产品主图片，本地文件路径 支持的文件类型：gif,jpg,png,jpeg</param>
        /// <param name="outerid">外部产品ID </param>
        /// <param name="props">属性,属性值的组合.格式:pid:vid;pid:vid;</param>
        /// <param name="binds">非关键属性结构:pid:vid;pid:vid. 最大支持512字节</param>
        /// <param name="saleprops">销售属性结构:pid:vid;pid:vid</param>
        /// <param name="customerprops">用户自定义属性,结构：pid1:value1;pid2:value2</param>
        /// <param name="price">产品市场价.精确到2位小数;单位为元.如：200.07</param>
        /// <param name="name">产品名称,最大30个字符</param>
        /// <param name="desc">产品描述.最大不超过25000个字符 </param>
        /// <param name="major">是不是主图 默认true</param>
        /// <param name="markettime">上市时间。目前只支持鞋城类目传入此参数</param>
        /// <param name="propertyalias"销售属性值别名。格式为pid1:vid1:alias1;pid1:vid2:alia2。只有少数销售属性值支持传入别名，比如颜色和尺寸></param>
        /// <param name="pakinglist">包装清单。注意，在管控类目下，包装清单不能为空，同时保证清单的格式为： 名称:数字;名称:数字; 其中，名称不能违禁、不能超过60字符，数字不能超过999 </param>
        /// <param name="extraingo">存放产品扩展信息，由List(ProductExtraInfo)转化成jsonArray存入. </param>
        /// <param name="markid">市场ID，1为新增C2C市场的产品信息， 2为新增B2C市场的产品信息。 不填写此值则C用户新增B2C市场的产品信息，B用户新增B2C市场的产品信息。</param>
        /// <param name="sellpt">商品卖点描述，长度限制为20个汉字 </param>
        /// <returns></returns>
        public static Product AddProduct(long? cid, string imagefilepath, string outerid, string props, string binds, string saleprops, string customerprops,
            string price, string name, string desc, bool? major, DateTime markettime, string propertyalias,
            string pakinglist, string extraingo, string markid, string sellpt)
        {
            ITopClient client = TopClientService.GetTopClient();
            ProductAddRequest req = new ProductAddRequest();
            req.Cid = cid;
            FileItem fItem = new FileItem(imagefilepath);
            req.Image = fItem;
            req.OuterId = outerid;
            req.Props = props;
            req.Binds = binds;
            req.SaleProps = saleprops;
            req.CustomerProps = customerprops;
            req.Price = price;
            req.Name = name;
            req.Desc = desc;
            req.Major = major;
            req.MarketTime = markettime;
            req.PropertyAlias = propertyalias;
            req.PackingList = pakinglist;
            req.ExtraInfo = extraingo;
            req.MarketId = markid;
            req.SellPt = sellpt;
            ProductAddResponse response = client.Execute(req, SessionKey);
            return response.Product;
        }

        /// <summary>
        /// taobao.itemprops.get
        /// 获取标准商品类目属性
        /// </summary>
        /// <param name="cid">必填 叶子类目ID，如果只传cid，则只返回一级属性</param>
        /// <param name="fields">需要返回的字段列表，见：ItemProp</param>
        /// <param name="pid">属性id (取类目属性时，传pid，不用同时传PID和parent_pid) </param>
        /// <param name="parentpid">父属性ID</param>
        /// <param name="iskeyprop">是否关键属性。</param>
        /// <param name="issaleprop">是否销售属性</param>
        /// <param name="iscolorprop">是否颜色属性</param>
        /// <param name="isenumprop">是否枚举属性 如果返回true，属性值是下拉框选择输入，如果返回false，属性值是用户自行手工输入。 </param>
        /// <param name="isinputprop">在is_enum_prop是true的前提下，是否是卖家可以自行输入的属性（注：如果is_enum_prop返回false，该参数统一返回false）。</param>
        /// <param name="isitemprop">是否商品属性，这个属性只能放于发布商品时使用</param>
        /// <param name="childpath">类目子属性路径,由该子属性上层的类目属性和类目属性值组成,格式pid:vid;pid:vid.取类目子属性需要传child_path,cid </param>
        /// <param name="type">获取类目的类型：1代表集市、2代表天猫 </param>
        /// <param name="attrkeys">属性的Key，支持多条，以“,”分隔 </param>
        /// <returns></returns>
        public static List<ItemProp> GetItemprops(long? cid, string fields, long? pid, long? parentpid, bool? iskeyprop, bool? issaleprop, bool? iscolorprop,
            bool? isenumprop, bool? isinputprop, bool? isitemprop, string childpath, long? type, string attrkeys)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItempropsGetRequest req = new ItempropsGetRequest();
            req.Fields = fields;
            req.Cid = cid;
            req.Pid = pid;
            req.ParentPid = parentpid;
            req.IsKeyProp = iskeyprop;
            req.IsSaleProp = issaleprop;
            req.IsColorProp = iscolorprop;
            req.IsEnumProp = isenumprop;
            req.IsInputProp = isinputprop;
            req.IsItemProp = isitemprop;
            req.ChildPath = childpath;
            req.Type = type;
            req.AttrKeys = attrkeys;
            ItempropsGetResponse response = client.Execute(req);
            return response.ItemProps;
        }

        /// <summary>
        /// taobao.itempropvalues.get
        /// 获取标准类目属性值
        /// </summary>
        /// <param name="fields">必填 需要返回的字段。目前支持有：cid,pid,prop_name,vid,name,name_alias,status,sort_order </param>
        /// <param name="cid">必填 叶子类目ID ,通过taobao.itemcats.get获得叶子类目ID</param>
        /// <param name="pvs">属性和属性值 id串，格式例如(pid1;pid2)或(pid1:vid1;pid2:vid2)或(pid1;pid2:vid2) </param>
        /// <param name="type">获取类目的类型：1代表集市、2代表天猫 </param>
        /// <param name="attrkeys">属性的Key，支持多条，以“,”分隔 </param>
        /// <returns></returns>
        public static List<PropValue> GetItempropValues(string fields, long? cid, string pvs, long? type, string attrkeys)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItempropvaluesGetRequest req = new ItempropvaluesGetRequest();
            req.Fields = fields;
            req.Cid = cid;
            req.Pvs = pvs;
            req.Type = type;
            req.AttrKeys = attrkeys;
            ItempropvaluesGetResponse response = client.Execute(req);
            return response.PropValues;
        }

        /// <summary>
        /// taobao.item.add
        /// 添加一个商品
        /// </summary>
        /// <param name="item">参考DataContract.ProductItem</param>
        /// <returns></returns>
        public static Item AddItem(ProductItem item)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemAddRequest req = new ItemAddRequest();
            req.Num = item.Num;
            req.Price = item.Price;
            req.Type = item.Type;
            req.StuffStatus = item.StuffStatus;
            req.Title = item.Title;
            req.Desc = item.Desc;
            req.LocationState = item.LocationState;
            req.LocationCity = item.LocationCity;
            req.ApproveStatus = item.ApproveStatus;
            req.Cid = item.Cid;
            req.Props = item.Props;
            req.FreightPayer = item.FreightPayer;
            req.ValidThru = item.ValidThru;
            req.HasInvoice = item.HasInvoice;
            req.HasWarranty = item.HasWarranty;
            req.HasShowcase = item.HasShowcase;
            req.SellerCids = item.SellerCids;
            req.HasDiscount = item.HasDiscount;
            req.PostFee = item.PostFee;
            req.ExpressFee = item.ExpressFee;
            req.EmsFee = item.EmsFee;
            req.ListTime = item.ListTime;
            req.Increment = item.Increment;
            FileItem fItem = new FileItem(item.ImgFilePath);
            req.Image = fItem;
            req.PostageId = item.PostageId;
            req.AuctionPoint = item.AuctionPoint;
            req.PropertyAlias = item.PropertyAlias;
            req.InputPids = item.InputPids;
            req.SkuProperties = item.SkuProperties;
            req.SkuQuantities = item.SkuQuantities;
            req.SkuPrices = item.SkuPrices;
            req.SkuOuterIds = item.SkuOuterIds;
            req.Lang = item.Lang;
            req.OuterId = item.OuterId;
            req.ProductId = item.ProductId;
            req.PicPath = item.PicPath;
            req.AutoFill = item.AutoFill;
            req.InputStr = item.InputStr;
            req.IsTaobao = item.IsTaobao;
            req.IsEx = item.IsEx;
            req.Is3D = item.Is3D;
            req.SellPromise = item.SellPromise;
            req.AfterSaleId = item.AfterSaleId;
            req.CodPostageId = item.CodPostageId;
            req.IsLightningConsignment = item.IsLightningConsignment;
            req.Weight = item.Weight;
            req.IsXinpin = item.IsXinpin;
            req.SubStock = item.SubStock;
            req.FoodSecurityPrdLicenseNo = item.FoodSecurityPrdLicenseNo;
            req.FoodSecurityDesignCode = item.FoodSecurityDesignCode;
            req.FoodSecurityFactory = item.FoodSecurityFactory;
            req.FoodSecurityFactorySite = item.FoodSecurityFactorySite;
            req.FoodSecurityContact = item.FoodSecurityContact;
            req.FoodSecurityMix = item.FoodSecurityMix;
            req.FoodSecurityPlanStorage = item.FoodSecurityPlanStorage;
            req.FoodSecurityPeriod = item.FoodSecurityPeriod;
            req.FoodSecurityFoodAdditive = item.FoodSecurityFoodAdditive;
            req.FoodSecuritySupplier = item.FoodSecuritySupplier;
            req.FoodSecurityProductDateStart = item.FoodSecurityProductDateStart;
            req.FoodSecurityProductDateEnd = item.FoodSecurityProductDateEnd;
            req.FoodSecurityStockDateStart = item.FoodSecurityStockDateStart;
            req.FoodSecurityStockDateEnd = item.FoodSecurityStockDateEnd;
            req.SkuSpecIds = item.SkuSpecIds;
            req.ScenicTicketPayWay = item.ScenicTicketPayWay;
            req.ScenicTicketBookCost = item.ScenicTicketBookCost;
            req.ItemSize = item.ItemSize;
            req.ItemWeight = item.ItemWeight;
            req.ChangeProp = item.ChangeProp;
            req.SellPoint = item.SellPoint;
            req.DescModules = item.DescModules;
            req.FoodSecurityHealthProductNo = item.FoodSecurityHealthProductNo;
            req.LocalityLifeChooseLogis = item.LocalityLifeChooseLogis;
            req.LocalityLifeExpirydate = item.LocalityLifeExpirydate;
            req.LocalityLifeNetworkId = item.LocalityLifeNetworkId;
            req.LocalityLifeMerchant = item.LocalityLifeMerchant;
            req.LocalityLifeVerification = item.LocalityLifeVerification;
            req.LocalityLifeRefundRatio = item.LocalityLifeRefundRatio;
            req.LocalityLifeOnsaleAutoRefundRatio = item.LocalityLifeOnsaleAutoRefundRatio;
            req.LocalityLifeRefundmafee = item.LocalityLifeRefundmafee;
            req.PaimaiInfoMode = item.PaimaiInfoMode;
            req.PaimaiInfoDeposit = item.PaimaiInfoDeposit;
            req.PaimaiInfoInterval = item.PaimaiInfoInterval;
            req.PaimaiInfoReserve = item.PaimaiInfoReserve;
            req.PaimaiInfoValidHour = item.PaimaiInfoValidHour;
            req.PaimaiInfoValidMinute = item.PaimaiInfoValidMinute;
            req.GlobalStockType = item.GlobalStockType;
            req.GlobalStockCountry = item.GlobalStockCountry;
            ItemAddResponse response = client.Execute(req, SessionKey);
            return response.Item;
        }

        /// <summary>
        /// taobao.item.img.upload
        /// 添加商品图片
        /// </summary>
        /// <param name="id">商品图片id(如果是更新图片，则需要传该参数) </param>
        /// <param name="numiid">必填 商品数字ID，该参数必须 </param>
        /// <param name="position">图片序号 </param>
        /// <param name="imgfilepath">上传的图片路径 支持的文件类型：gif,jpg,jpeg,png </param>
        /// <param name="ismajor">是否将该图片设为主图,可选值:true,false;默认值:false</param>
        /// <returns></returns>
        public static ItemImg UploadProductImg(long? id, long? numiid, long? position, string imgfilepath, bool? ismajor)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemImgUploadRequest req = new ItemImgUploadRequest();
            req.Id = id;
            req.NumIid = numiid;
            req.Position = position;
            FileItem fItem = new FileItem(imgfilepath);
            req.Image = fItem;
            req.IsMajor = ismajor;
            ItemImgUploadResponse response = client.Execute(req, SessionKey);
            return response.ItemImg;
        }
        #endregion

        #region 商品信息修改需要用到的方法
        /// <summary>
        /// 获取一个产品的信息
        /// 两种方式查看一个产品详细信息: 传入product_id来查询 传入cid和props来查询
        /// </summary>
        /// <param name="fields">必填 需返回的字段列表.可选值:Product数据结构中的所有字段;多个字段之间用","分隔</param>
        /// <param name="productid">Product的id.两种方式来查看一个产品:1.传入product_id来查询 2.传入cid和props来查询</param>
        /// <param name="cid">商品类目id.调用taobao.itemcats.get获取;必须是叶子类目id,如果没有传product_id,那么cid和props必须要传</param>
        /// <param name="props">关键属性,结构：pid1:value1;pid2:value2</param>
        /// <param name="customerprops">用户自定义关键属性,结构：pid1:value1;pid2:value2，如果有型号，系列等子属性用: 隔开</param>
        /// <param name="marketid">市场ID，1为取C2C市场的产品信息， 2为取B2C市场的产品信息。 不填写此值则默认取C2C的产品信息。</param>
        /// <returns></returns>
        public static Product GetProductInfo(string fields, long? productid, long? cid, string props, string customerprops, string marketid)
        {
            ITopClient client = TopClientService.GetTopClient();
            ProductGetRequest req = new ProductGetRequest();
            req.Fields = fields;
            req.ProductId = productid;
            req.Cid = cid;
            req.Props = props;
            req.CustomerProps = customerprops;
            req.MarketId = marketid;
            ProductGetResponse response = client.Execute(req);
            return response.Product;
        }

        /// <summary>
        /// taobao.items.onsale.get
        /// 获取当前会话用户出售中的商品列表
        /// </summary>
        /// <param name="fields">需返回的字段列表。可选值：Item商品结构体中的以下字段： approve_status,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id；字段之间用“,”分隔。
        /// 不支持其他字段，如果需要获取其他字段数据，调用taobao.item.get。 </param>
        /// <param name="keywords">搜索字段。搜索商品的title。 </param>
        /// <param name="cid">商品类目ID。ItemCat中的cid字段。可以通过taobao.itemcats.get取到 </param>
        /// <param name="sellercids">卖家店铺内自定义类目ID。多个之间用“,”分隔。可以根据taobao.sellercats.list.get获得.(注：目前最多支持32个ID号传入) </param>
        /// <param name="pageno">页码。取值范围:大于零的整数。默认值为1,即默认返回第一页数据。用此接口获取数据时，当翻页获取的条数（page_no*page_size）超过10万,为了保护后台搜索引擎，接口将报错。所以请大家尽可能的细化自己的搜索条件，例如根据修改时间分段获取商品 </param>
        /// <param name="pagesize">每页条数。取值范围:大于零的整数;最大值：200；默认值：40。用此接口获取数据时，当翻页获取的条数（page_no*page_size）超过2万,为了保护后台搜索引擎，接口将报错。所以请大家尽可能的细化自己的搜索条件，例如根据修改时间分段获取商品 </param>
        /// <param name="hasdiscount">是否参与会员折扣。可选值：true，false。默认不过滤该条件 </param>
        /// <param name="hasshowcase">是否橱窗推荐。 可选值：true，false。默认不过滤该条件 </param>
        /// <param name="orderby">排序方式。格式为column:asc/desc ，column可选值:list_time(上架时间),delist_time(下架时间),num(商品数量)，modified(最近修改时间);默认上架时间降序(即最新上架排在前面)。如按照上架时间降序排序方式为list_time:desc </param>
        /// <param name="istaobao">商品是否在淘宝显示 </param>
        /// <param name="isex">商品是否在外部网店显示 </param>
        /// <param name="startmodified">起始的修改时间 </param>
        /// <param name="endmodified">结束的修改时间</param>
        /// <returns></returns>
        public static List<Item> GetOnsaleItem(string fields, string keywords, long? cid, string sellercids, long? pageno, long? pagesize, bool? hasdiscount,
            bool? hasshowcase, string orderby, bool? istaobao, bool? isex, DateTime? startmodified, DateTime? endmodified)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemsOnsaleGetRequest req = new ItemsOnsaleGetRequest();
            req.Fields = fields;
            req.Q = keywords;
            req.Cid = cid;
            req.SellerCids = sellercids;
            req.PageNo = pageno;
            req.HasDiscount = hasdiscount;
            req.HasShowcase = hasshowcase;
            req.OrderBy = orderby;
            req.IsTaobao = istaobao;
            req.IsEx = isex;
            req.PageSize = pagesize;
            req.StartModified = startmodified;
            req.EndModified = endmodified;
            ItemsOnsaleGetResponse response = client.Execute(req, SessionKey);
            return response.Items;
        }

        /// <summary>
        /// taobao.items.inventory.get
        /// 得到当前会话用户库存中的商品列表
        /// </summary>
        /// <param name="fields">需返回的字段列表。可选值：Item商品结构体中的以下字段： approve_status,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id；字段之间用“,”分隔。
        /// 不支持其他字段，如果需要获取其他字段数据，调用taobao.item.get。 </param>
        /// <param name="keywords">搜索字段。搜索商品的title。 </param>
        /// <param name="banner">分类字段。可选值:regular_shelved(定时上架) never_on_shelf(从未上架) off_shelf(我下架的) for_shelved(等待所有上架) sold_out(全部卖完) violation_off_shelf(违规下架的)
        ///  默认查询for_shelved(等待所有上架)这个状态的商品
        ///  注：for_shelved(等待所有上架)=regular_shelved(定时上架)+never_on_shelf(从未上架)+off_shelf(我下架的) </param>
        /// <param name="cid">商品类目ID。ItemCat中的cid字段。可以通过taobao.itemcats.get取到 </param>
        /// <param name="sellercids">卖家店铺内自定义类目ID。多个之间用“,”分隔。可以根据taobao.sellercats.list.get获得.(注：目前最多支持32个ID号传入) </param>
        /// <param name="pageno">页码。取值范围:大于零的整数。默认值为1,即默认返回第一页数据。用此接口获取数据时，当翻页获取的条数（page_no*page_size）超过10万,为了保护后台搜索引擎，接口将报错。所以请大家尽可能的细化自己的搜索条件，例如根据修改时间分段获取商品 </param>
        /// <param name="pagesize">每页条数。取值范围:大于零的整数;最大值：200；默认值：40。用此接口获取数据时，当翻页获取的条数（page_no*page_size）超过2万,为了保护后台搜索引擎，接口将报错。所以请大家尽可能的细化自己的搜索条件，例如根据修改时间分段获取商品 </param>
        /// <param name="hasdiscount">是否参与会员折扣。可选值：true，false。默认不过滤该条件 </param>
        /// <param name="orderby">排序方式。格式为column:asc/desc ，column可选值:list_time(上架时间),delist_time(下架时间),num(商品数量)，modified(最近修改时间);默认上架时间降序(即最新上架排在前面)。如按照上架时间降序排序方式为list_time:desc </param>
        /// <param name="istaobao">商品是否在淘宝显示 </param>
        /// <param name="isex">商品是否在外部网店显示 </param>
        /// <param name="startmodified">起始的修改时间 </param>
        /// <param name="endmodified">结束的修改时间</param>
        /// <returns></returns>
        public static List<Item> GetInventoryItem(string fields, string keywords, string banner, long? cid, string sellercids, long? pageno, long? pagesize,
            bool? hasdiscount, string orderby, bool? istaobao, bool? isex, DateTime? startmodified, DateTime? endmodified)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemsInventoryGetRequest req = new ItemsInventoryGetRequest();
            req.Fields = fields;
            req.Q = keywords;
            req.Banner = banner;
            req.Cid = cid;
            req.SellerCids = sellercids;
            req.PageNo = pageno;
            req.PageSize = pagesize;
            req.HasDiscount = hasdiscount;
            req.OrderBy = orderby;
            req.IsTaobao = istaobao;
            req.IsEx = isex;
            req.StartModified = startmodified;
            req.EndModified = endmodified;
            ItemsInventoryGetResponse response = client.Execute(req, SessionKey);
            return response.Items;
        }

        /// <summary>
        /// taobao.items.custom.get
        /// 根据商家编码取商品
        /// </summary>
        /// <param name="outid">商品的外部商品ID，支持批量，最多不超过40个。</param>
        /// <param name="fields">需返回的字段列表。可选值：Item商品结构体中的所有字段；多个字段之间用“,”分隔。
        /// 如果想返回整个子对象，那字段为item_img，如果是想返回子对象里面的字段，那字段为item_img.url。
        /// 新增返回字段：one_station标记商品是否淘1站商品 </param>
        /// <returns></returns>
        public static List<Item> GetItemsByCustomId(string outid, string fields)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemsCustomGetRequest req = new ItemsCustomGetRequest();
            req.OuterId = outid;
            req.Fields = fields;
            ItemsCustomGetResponse response = client.Execute(req, SessionKey);
            return response.Items;
        }

        /// <summary>
        /// taobao.skus.custom.get
        /// 根据sku的商家编码取商品sku
        /// </summary>
        /// <param name="outid">Sku的外部商家ID </param>
        /// <param name="fields">需返回的字段列表。可选值：Sku结构体中的所有字段；字段之间用“,”隔开 </param>
        /// <returns></returns>
        public static List<Sku> GetSkusByCustomId(string outid, string fields)
        {
            ITopClient client = TopClientService.GetTopClient();
            SkusCustomGetRequest req = new SkusCustomGetRequest();
            req.OuterId = outid;
            req.Fields = fields;
            SkusCustomGetResponse response = client.Execute(req, SessionKey);
            return response.Skus;
        }

        /// taobao.increment.items.get
        /// 获取商品变更通知信息
        /// </summary>
        /// <param name="status">商品操作状态，默认查询所有状态的数据，除了默认值外，每次可查询多种状态，每种状态间用英语逗号分隔。具体类型列表见：
        /// ItemAdd（新增商品） ItemUpshelf（上架商品，自动上架商品不能获取到增量信息） ItemDownshelf（下架商品） ItemDelete（删除商品） ItemUpdate（更新商品）
        /// ItemRecommendDelete（取消橱窗推荐商品） ItemRecommendAdd（橱窗推荐商品） ItemZeroStock（商品卖空） ItemPunishDelete（小二删除商品） ItemPunishDownshelf（小二下架商品）
        /// ItemPunishCc（小二CC商品） ItemSkuZeroStock（商品SKU卖空） ItemStockChanged（修改商品库存） </param>
        /// <param name="nick">消息所属于的用户的昵称。设置此参数，返回的消息会根据传入nick的进行过滤。自用型AppKey的昵称默认为自己的绑定昵称，此参数无效。</param>
        /// <param name="startmodified">消息所对应的操作时间的最小值和end_modified搭配使用能过滤消通知消息的时间段。不传时：如果设置了end_modified，默认为与 end_modified同一天的00:00:00，否则默认为调用接口当天的00:00:00。（格式：yyyy-MM-dd HH:mm:ss）
        /// 最早可取6天内的数据。 注意：start_modified和end_modified的日期必须在必须在同一天内，比如：start_modified设置2000-01-01 00:00:00，则end_modified必须设置为2000-01-01这个日期 </param>
        /// <param name="endmodified">消息所对应的操作时间的最大值。和start_modified搭配使用能过滤消通知消息的时间段。不传时：如果设置了start_modified，默认为与start_modified同一天的23:59:59；否则默认为调用接口当天的23:59:59。（格式：yyyy-MM-dd HH:mm:ss）
        /// 注意：start_modified和end_modified的日期必须在必须在同一天内，比如：start_modified设置2000-01-01 00:00:00，则end_modified必须设置为2000-01-01这个日期 </param>
        /// <param name="pageno">页码。取值范围:大于零的整数; 默认值:1,即返回第一页数据。 </param>
        /// <param name="pagesize">每页条数。取值范围:大于零的整数;最大值:200;默认值:40。 </param>
        /// <returns></returns>
        public static List<NotifyItem> GetIncrementItems(string status, string nick, DateTime? startmodified, DateTime? endmodified, long? pageno, long? pagesize)
        {
            ITopClient client = TopClientService.GetTopClient();
            IncrementItemsGetRequest req = new IncrementItemsGetRequest();
            req.Status = status;
            req.Nick = nick;
            req.StartModified = startmodified;
            req.EndModified = endmodified;
            req.PageNo = pageno;
            req.PageSize = pagesize;
            IncrementItemsGetResponse response = client.Execute(req);
            return response.NotifyItems;
        }

        /// <summary>
        /// taobao.item.update
        /// 更新商品信息
        /// </summary>
        /// <param name="item">参考DataContract.ProductItem</param>
        /// <returns></returns>
        public static Item UpdateItem(ProductItem item)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemUpdateRequest req = new ItemUpdateRequest();
            req.NumIid = item.NumIid;
            req.Cid = item.Cid;
            req.Props = item.Props;
            req.Num = item.Num;
            req.Price = item.Price;
            req.Title = item.Title;
            req.Desc = item.Desc;
            req.LocationState = item.LocationState;
            req.LocationCity = item.LocationCity;
            req.PostFee = item.PostFee;
            req.ExpressFee = item.ExpressFee;
            req.EmsFee = item.EmsFee;
            req.ListTime = item.ListTime;
            req.Increment = item.Increment;
            FileItem fItem = new FileItem(item.ImgFilePath);
            req.Image = fItem;
            req.StuffStatus = item.StuffStatus;
            req.AuctionPoint = item.AuctionPoint;
            req.PropertyAlias = item.PropertyAlias;
            req.InputPids = item.InputPids;
            req.SkuQuantities = item.SkuQuantities;
            req.SkuPrices = item.SkuPrices;
            req.SkuProperties = item.SkuProperties;
            req.SellerCids = item.SellerCids;
            req.PostageId = item.PostageId;
            req.OuterId = item.OuterId;
            req.ProductId = item.ProductId;
            req.PicPath = item.PicPath;
            req.AutoFill = item.AutoFill;
            req.SkuOuterIds = item.SkuOuterIds;
            req.IsTaobao = item.IsTaobao;
            req.IsEx = item.IsEx;
            req.Is3D = item.Is3D;
            req.IsReplaceSku = item.IsReplaceSku;
            req.InputStr = item.InputStr;
            req.Lang = item.Lang;
            req.HasDiscount = item.HasDiscount;
            req.HasShowcase = item.HasShowcase;
            req.ApproveStatus = item.ApproveStatus;
            req.FreightPayer = item.FreightPayer;
            req.ValidThru = item.ValidThru;
            req.HasInvoice = item.HasInvoice;
            req.HasWarranty = item.HasWarranty;
            req.AfterSaleId = item.AfterSaleId;
            req.SellPromise = item.SellPromise;
            req.CodPostageId = item.CodPostageId;
            req.IsLightningConsignment = item.IsLightningConsignment;
            req.Weight = item.Weight;
            req.IsXinpin = item.IsXinpin;
            req.SubStock = item.SubStock;
            req.FoodSecurityPrdLicenseNo = item.FoodSecurityPrdLicenseNo;
            req.FoodSecurityDesignCode = item.FoodSecurityDesignCode;
            req.FoodSecurityFactory = item.FoodSecurityFactory;
            req.FoodSecurityFactorySite = item.FoodSecurityFactorySite;
            req.FoodSecurityContact = item.FoodSecurityContact;
            req.FoodSecurityMix = item.FoodSecurityMix;
            req.FoodSecurityPlanStorage = item.FoodSecurityPlanStorage;
            req.FoodSecurityPeriod = item.FoodSecurityPeriod;
            req.FoodSecurityFoodAdditive = item.FoodSecurityFoodAdditive;
            req.FoodSecuritySupplier = item.FoodSecuritySupplier;
            req.FoodSecurityProductDateStart = item.FoodSecurityProductDateStart;
            req.FoodSecurityProductDateEnd = item.FoodSecurityProductDateEnd;
            req.FoodSecurityStockDateStart = item.FoodSecurityStockDateStart;
            req.FoodSecurityStockDateEnd = item.FoodSecurityStockDateEnd;
            req.SkuSpecIds = item.SkuSpecIds;
            req.ItemSize = item.ItemSize;
            req.ItemWeight = item.ItemWeight;
            req.ChangeProp = item.ChangeProp;
            req.SellPoint = item.SellPoint;
            req.DescModules = item.DescModules;
            req.FoodSecurityHealthProductNo = item.FoodSecurityHealthProductNo;
            req.EmptyFields = item.EmptyFields;
            req.LocalityLifeExpirydate = item.LocalityLifeExpirydate;
            req.LocalityLifeNetworkId = item.LocalityLifeNetworkId;
            req.LocalityLifeMerchant = item.LocalityLifeMerchant;
            req.LocalityLifeVerification = item.LocalityLifeVerification;
            req.LocalityLifeRefundRatio = item.LocalityLifeRefundRatio;
            req.LocalityLifeChooseLogis = item.LocalityLifeChooseLogis;
            req.LocalityLifeOnsaleAutoRefundRatio = item.LocalityLifeOnsaleAutoRefundRatio;
            req.LocalityLifeRefundmafee = item.LocalityLifeRefundmafee;
            req.ScenicTicketPayWay = item.ScenicTicketPayWay;
            req.ScenicTicketBookCost = item.ScenicTicketBookCost;
            req.PaimaiInfoMode = item.PaimaiInfoMode;
            req.PaimaiInfoDeposit = item.PaimaiInfoDeposit;
            req.PaimaiInfoInterval = item.PaimaiInfoInterval;
            req.PaimaiInfoReserve = item.PaimaiInfoReserve;
            req.PaimaiInfoValidHour = item.PaimaiInfoValidHour;
            req.PaimaiInfoValidMinute = item.PaimaiInfoValidMinute;
            req.GlobalStockType = item.GlobalStockType;
            req.GlobalStockCountry = item.GlobalStockCountry;
            ItemUpdateResponse response = client.Execute(req, SessionKey);
            return response.Item;
        }

        /// <summary>
        /// taobao.item.sku.update
        /// 更新SKU信息
        /// </summary>
        /// <param name="numid">必填 Sku所属商品数字id，可通过 taobao.item.get 获取 </param>
        /// <param name="properties">必填 Sku属性串。格式:pid:vid;pid:vid,如: 1627207:3232483;1630696:3284570,表示机身颜色:军绿色;手机套餐:一电一充。
        /// 如果包含自定义属性，则格式为pid:vid;pid2:vid2;$pText:vText , 其中$pText:vText为自定义属性。限制：其中$pText的’$’前缀不能少，且pText和vText文本中不可以存在 冒号:和分号;以及逗号， </param>
        /// <param name="quantity">Sku的库存数量。sku的总数量应该小于等于商品总数量(Item的NUM)，sku数量变化后item的总数量也会随着变化。取值范围:大于等于零的整数 </param>
        /// <param name="price">Sku的销售价格。精确到2位小数;单位:元。如:200.07，表示:200元7分。修改后的sku价格要保证商品的价格在所有sku价格所形成的价格区间内
        /// （例如：商品价格为6元，sku价格有5元、10元两种，如果要修改5元sku的价格，那么修改的范围只能是0-6元之间；如果要修改10元的sku，那么修改的范围只能是6到无穷大的区间中）</param>
        /// <param name="outerid">Sku的商家外部id </param>
        /// <param name="itemprice">sku所属商品的价格。当用户更新sku，使商品价格不属于sku价格之间的时候，用于修改商品的价格，使sku能够更新成功 </param>
        /// <param name="lang">Sku文字的版本。可选值:zh_HK(繁体),zh_CN(简体);默认值:zh_CN </param>
        /// <param name="specid">产品的规格信息。 </param>
        /// <returns></returns>
        public static Sku UpdateItemSku(long? numid, string properties, long? quantity, string price, string outerid, string itemprice, string lang, string specid)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemSkuUpdateRequest req = new ItemSkuUpdateRequest();
            req.NumIid = numid;
            req.Properties = properties;
            req.Quantity = quantity;
            req.Price = price;
            req.OuterId = outerid;
            req.ItemPrice = itemprice;
            req.Lang = lang;
            req.SpecId = specid;
            ItemSkuUpdateResponse response = client.Execute(req, SessionKey);
            return response.Sku;
        }

        /// <summary>
        /// taobao.item.update.delisting
        /// 商品下架
        /// </summary>
        /// <param name="numiid">商品数字ID，该参数必须 </param>
        /// <returns></returns>
        public static Item DelistingItem(long? numiid)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemUpdateDelistingRequest req = new ItemUpdateDelistingRequest();
            req.NumIid = numiid;
            ItemUpdateDelistingResponse response = client.Execute(req, SessionKey);
            return response.Item;
        }

        /// <summary>
        /// taobao.item.update.listing
        /// 一口价商品上架
        /// </summary>
        /// <param name="numiid">商品数字ID，该参数必须 </param>
        /// <param name="num">必填 需要上架的商品的数量。取值范围:大于零的整数。
        /// 如果商品有sku，则上架数量默认为所有sku数量总和，不可修改。否则商品数量根据设置数量调整为num </param>
        /// <returns></returns>
        public static Item ListingItem(long? numiid, long num)
        {
            ITopClient client = TopClientService.GetTopClient();
            ItemUpdateListingRequest req = new ItemUpdateListingRequest();
            req.NumIid = numiid;
            req.Num = num;
            ItemUpdateListingResponse response = client.Execute(req, SessionKey);
            return response.Item;
        }

        #endregion
    }
}
